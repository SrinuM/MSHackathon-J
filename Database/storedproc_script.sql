create PROCEDURE spGetBuildingData  
    -- Add the parameters for the stored procedure here  
    @id int  
      
AS  
BEGIN  
    -- SET NOCOUNT ON added to prevent extra result sets from  
    -- interfering with SELECT statements.  
    --SET NOCOUNT ON;  
    
select Floorid,floorname, count(1) Slotcount from FLOOR
 where buildingid =@id
group by buildingid,floorid,floorname
          
END 




alter PROCEDURE spGetSlotinformation  
    -- Add the parameters for the stored procedure here  
    @buildingid int , 
     @floorid int 
AS  
BEGIN  
    -- SET NOCOUNT ON added to prevent extra result sets from  
    -- interfering with SELECT statements.  
    --SET NOCOUNT ON;  
  select s.slotid,s.slotname, s.status from slot s
join floor f on s.slotid = f.slotid
join building b on b.id = f.buildingid
where b.id = @buildingid and f.floorid= @floorid
          
END 



DECLARE	@return_value int

EXEC	@return_value = [dbo].[spGetBuildingData]
		@id = 1


SELECT	'Return Value' = @return_value

GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[spGetSlotinformation]
		@buildingid = 1,
@floorid =1

SELECT	'Return Value' = @return_value

GO