GO
DECLARE	@return_value int,
		@Result uniqueidentifier

EXEC	@return_value = [Setup].[usp_V1_BrandInfo_Save]
		@Id = NULL,
		@Name = 'TEST',
		@LoggedInUserId = N'F6510A9A-2E3D-4341-9E94-090ACC25D2A5',
		@Result = @Result OUTPUT

SELECT	@Result as N'@Result'

SELECT	'Return Value' = @return_value

GO
