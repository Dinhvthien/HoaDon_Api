namespace HoaDon_Api.PayLoads.Responses
{
    public class ResponseObject<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public ResponseObject() { }
        public ResponseObject(int status,string message,T data)
        {
            status = status;
            message = message;
            data = data;
        }
        public ResponseObject<T> ResponseSuccses(string message,T data)
        {
            return new ResponseObject<T>(StatusCodes.Status200OK,message,data);
        }
        public ResponseObject<T> ResponseError(int status,string message, T data)
        {
            return new ResponseObject<T>(status, message, data);
        }
    }
}
