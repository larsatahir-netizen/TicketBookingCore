
namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {
        private readonly ITicketBookingRepository _ticketBookingRepository;

        public TicketBookingRequestProcessor(
            ITicketBookingRepository ticketBookingRepository)

        {
            _ticketBookingRepository = ticketBookingRepository;
        }


        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var response = Create<TicketBookingResponse>(request);

            if (!WorksEmail(request.Email))
            {
                response.Works = false;
                return response;
            }

            //kod för att spara i databasen
            _ticketBookingRepository.Save(Create<TicketBooking>(request));
            response.Works = true;
            return Create<TicketBookingResponse>(request);
        }

        private static T Create<T>(TicketBookingRequest request) where T : TicketBookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
        }

    }
}
