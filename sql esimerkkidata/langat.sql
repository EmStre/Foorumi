USE [Foorumi]
GO

INSERT INTO [dbo].[Langat]
           ([kayttaja_id]
           ,[alue_id]
           ,[aika]
           ,[otsikko]
           ,[lukittu]
           ,[kiinnitetty])
     VALUES
           (<kayttaja_id, int,>
           ,<alue_id, int,>
           ,<aika, datetime,>
           ,<otsikko, nvarchar(50),>
           ,<lukittu, bit,>
           ,<kiinnitetty, bit,>)
GO

