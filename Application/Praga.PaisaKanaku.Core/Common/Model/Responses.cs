using Newtonsoft.Json.Linq;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Enum;

namespace Praga.PaisaKanaku.Core.Common.Model
{    
    public class Response<T>
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }

        public ResultTypeEnum StatusCode { get; set; }

        public string? Message { get; set; }

        public List<string> ValidationErrorMessages { get; set; } = new List<string>();


        //public static Response<T> None => new Response<T>();

        public bool HasValidationErrorMessages => ValidationErrorMessages.Count > 0;

        public Response<T> GetNotAuthorizedResponse() => new() { StatusCode = ResultTypeEnum.NotAuthorized, Message = ResponseConstants.INVALID_LOGGED_IN_USER};
        
        public Response<T> GetNotFoundResponse() => new() { StatusCode = ResultTypeEnum.NotFound, Message = ResponseConstants.NO_RECORDS_FOUND };

        public Response<T> GetFailedResponse(string errorMessage) => new() { StatusCode = ResultTypeEnum.Failed, Message = errorMessage };

        public Response<T> GetValidationFailedResponse() => new() { StatusCode = ResultTypeEnum.ValidationError, Message = ResponseConstants.VALIDATION_FAILED, ValidationErrorMessages = this.ValidationErrorMessages };

        public Response<T> GetSuccessResponse(T data) => new() { Data = data, StatusCode = ResultTypeEnum.Success, IsSuccess = true, Message = ResponseConstants.SUCCESS };
    }
}

