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

        public Response<T> GetNotFoundResponse() => new() { StatusCode = ResultTypeEnum.NotFound };

        public Response<T> GetFailedResponse(string errorMessage) => new() { StatusCode = ResultTypeEnum.Failed, Message = errorMessage };

        public Response<T> GetValidationFailedResponse(List<string> validationErrorMessages) => new() { StatusCode = ResultTypeEnum.ValidationError, ValidationErrorMessages = validationErrorMessages };

        public Response<T> GetSuccessResponse(T data) => new() { Data = data, StatusCode = ResultTypeEnum.Success, IsSuccess = true, Message = ResponseConstants.SUCCESS };
    }
}

