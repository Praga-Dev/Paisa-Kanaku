GO

DECLARE	@return_value int

EXEC	@return_value = [Setup].[usp_V1_BrandInfo_Get]
		@LoggedInUserId =  N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5'

SELECT	'Return Value' = @return_value

GO