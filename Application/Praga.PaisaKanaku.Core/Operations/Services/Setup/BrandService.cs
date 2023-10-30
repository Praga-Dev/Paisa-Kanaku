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
    public class BrandService : IBrandService
    {
        private readonly ILogger<BrandService> _logger;

        private readonly IBrandRepository _brandRepository;

        public BrandService(ILogger<BrandService> logger, IBrandRepository brandRepository)
        {
            _logger = logger;
            _brandRepository = brandRepository;
        }

        public async Task<Response<BrandInfo>> GetBrandInfoById(Guid brandInfoId, Guid loggedInUserId)
        {
            Response<BrandInfo> response = new Response<BrandInfo>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (!Helpers.IsValidGuid(brandInfoId))
                {
                    return response;
                }

                Response<BrandInfoDB> dbResponse = await _brandRepository.GetBrandInfoById(brandInfoId, loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = new BrandInfo()
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
                _logger.LogError(ex, "Error in BrandService.GetBrandInfoList({@brandInfoId}, {@loggedInUserId})", brandInfoId, loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<List<BrandInfo>>> GetBrandInfoList(Guid loggedInUserId)
        {
            Response<List<BrandInfo>> response = new Response<List<BrandInfo>>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {

                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                var dbResponse = await _brandRepository.GetBrandInfoList(loggedInUserId);
                if (Helpers.IsResponseValid(dbResponse))
                {
                    response.Data = dbResponse.Data
                  .Select(brand => new BrandInfo()
                  {
                      Id = brand.Id,
                      Name = brand.Name,
                      SequenceId = brand.SequenceId,
                      CreatedBy = brand.CreatedBy,
                      CreatedDate = brand.CreatedDate,
                      ModifiedBy = brand.ModifiedBy,
                      ModifiedDate = brand.ModifiedDate,
                      RowStatus = brand.RowStatus
                  }).ToList();

                    response = response.GetSuccessResponse(response.Data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.GetBrandInfoList({@loggedInUserId})", loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
            }

            return response;
        }

        public async Task<Response<Guid>> SaveBrandInfo(BrandInfo brandInfo, bool isUpdate, Guid loggedInUserId)
        {
            Response<Guid> response = new Response<Guid>().GetFailedResponse(ResponseConstants.INVALID_PARAM);

            try
            {
                if (!Helpers.IsValidGuid(loggedInUserId))
                {
                    response.Message = ResponseConstants.INVALID_LOGGED_IN_USER;
                    return response;
                }

                if (brandInfo == null)
                {
                    response.Message = ResponseConstants.INVALID_PARAM;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(brandInfo.Name))
                {
                    response.ValidationErrorMessages.Add("Invalid Brand Name");
                }

                if (brandInfo.Name.Length < 2 || brandInfo.Name.Length > 50)
                {
                    response.ValidationErrorMessages.Add("Brand Name must be between 2 and 50 Characters long.");
                }

                BrandInfoDB brandInfoDb = new BrandInfoDB()
                {
                    Name = brandInfo.Name
                };

                if (isUpdate)
                {
                    if (!Helpers.IsValidGuid(brandInfo.Id))
                    {
                        response.Message = ResponseConstants.INVALID_ID;
                        response.ValidationErrorMessages = new();
                        return response;
                    }

                    brandInfoDb.Id = brandInfo.Id;
                }

                return await _brandRepository.SaveBrandInfo(brandInfoDb, loggedInUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.SaveBrandInfo({@brandInfo}, {@loggedInUserId})", brandInfo.ToString(), loggedInUserId);
                response = response.GetFailedResponse(ResponseConstants.INTERNAL_SERVER_ERROR);
                return response;
            }

        }
    }
}
