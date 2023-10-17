using System.Net;

namespace BookingManagementApp.Utilities.Handlers
{
    public class ResponseOKHandler<TEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity? Data { get; set; }

        public ResponseOKHandler(TEntity? data, string? message = null)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message ?? "Success to Retrieve Data";
            Data = data;
        }

        public ResponseOKHandler(string message)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
        }

        public ResponseOKHandler()
        {

        }
    }
}
