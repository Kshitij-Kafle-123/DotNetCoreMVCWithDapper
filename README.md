# DotNetCoreMVCWithDapper




Database queries.

1. "For Insert Operation"

USE [MVCDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Usp_Insert]

  @Name varchar(50),
  @Quantity int,
  @Color varchar(50),
  @Price decimal(18,2),
  @ProductCode varchar(50)

AS
BEGIN


    insert into Product(Name, Quantity,Color, Price, ProductCode) 
	values(@Name, @Quantity, @Color, @Price, @ProductCode)
END


2. "For Delete Operation"

USE [MVCDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Usp_Delete]
	@ProductId int
AS
BEGIN
    delete from Product where ProductId= @ProductId
END

3. "For Update"

USE [MVCDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Usp_Update] 
	@ProductId int,
	@Name varchar(50),
	@Quantity int,
	@Color varchar(50),
    @Price decimal(18,2),
    @ProductCode varchar(50)
AS
BEGIN
	update Product set 
	Name=@Name,
	Quantity=@Quantity,
	Color=@Color,
	Price=@Price,
	ProductCode=@ProductCode
	where ProductId=@ProductId
	
END


4."For Select All"


USE [MVCDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Usp_GetAllProducts]

AS	
BEGIN
	
    select * from Product
END

5."Select By Product Id"


USE [MVCDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Usp_GetProductById]

@ProductId int

AS
BEGIN
    select * from Product where ProductId = @ProductId
END


6."Check the existence of product"


USE [MVCDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Usp_CheckProduct]

@ProductId int
	
AS
BEGIN
	 IF EXISTS (SELECT * FROM Product WHERE ProductId = @ProductId)
        SELECT 1
    ELSE
        SELECT 0 
END
