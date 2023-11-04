CREATE FUNCTION [Common].[udp_v1_ValidateMonth]
(
	@Month INT
)
RETURNS BIT
AS
BEGIN
	RETURN CASE WHEN (@Month <= 0 OR @Month > 12) THEN 0 ELSE 1 END;
END
