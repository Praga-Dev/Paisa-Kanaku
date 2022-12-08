using Microsoft.Extensions.Logging;
using Praga.PaisaKanaku.Core.Common.Constants;
using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.Common.Utils;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.Operations.Services.Setup
{
    public class BillTypeService : IBillTypeService
    {
        private readonly ILogger<BillTypeService> _logger;

        private readonly IBillTypeRepository _billTypeRepository;

        public BillTypeService(ILogger<BillTypeService> logger, IBillTypeRepository billTypeRepository)
        {
            _logger = logger;
            _billTypeRepository = billTypeRepository;
        }

        public async Task<Response<BillTypeInfo>> GetBillTypeInfoById(Guid billTypeInfoId, Guid loggedInUserId)
        {
            Response<BillTypeInfo> response = new Response<BillTypeInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(billTypeInfoId))
                {
                    return response;
                }

                Response<BillTypeInfoDb> dbResponse = await _billTypeRepository.GetBillTypeInfoById(billTypeInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new BillTypeInfo()
                    {
                        Id = dbResponse.Data.Id,
                        Name = dbResponse.Data.Name,
                        SequenceId = dbResponse.Data.SequenceId,
                        CreatedBy = dbResponse.Data.CreatedBy,
                        CreatedDate = dbResponse.Data.CreatedDate,
                        ModifiedBy = dbResponse.Data.ModifiedBy,
                        ModifiedDate = dbResponse.Data.ModifiedDate,
                        RowStatus = dbResponse.Data.RowStatus
                    };

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeService.GetBillTypeInfoList({@billTypeInfoId}, {@loggedInUserId})", billTypeInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<BillTypeInfo>>> GetBillTypeInfoList(Guid loggedInUserId)
        {
            Response<List<BillTypeInfo>> response = new Response<List<BillTypeInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _billTypeRepository.GetBillTypeInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(billType => new BillTypeInfo()
                  {
                      Id = billType.Id,
                      Name = billType.Name,
                      SequenceId = billType.SequenceId,
                      CreatedBy = billType.CreatedBy,
                      CreatedDate = billType.CreatedDate,
                      ModifiedBy = billType.ModifiedBy,
                      ModifiedDate = billType.ModifiedDate,
                      RowStatus = billType.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeService.GetBillTypeInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveBillTypeInfo(BillTypeInfo billTypeInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (billTypeInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(billTypeInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid BillType Name");
                }

                if (billTypeInfo.Name.Length < 2 || billTypeInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("BillType Name must be between 2 and 50 Characters long.");
                }

                BillTypeInfoDb billTypeInfoDb = new BillTypeInfoDb()
                {
                    Name = billTypeInfo.Name
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(billTypeInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    billTypeInfoDb.Id = billTypeInfo.Id;
                }

                return await _billTypeRepository.SaveBillTypeInfo(billTypeInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BillTypeService.SaveBillTypeInfo({@billTypeInfo}, {@loggedInUserId})", billTypeInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }
    }
}
