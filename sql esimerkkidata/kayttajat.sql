USE [Foorumi]
GO

INSERT INTO [dbo].[Kayttajat]
           ([kayttajataso_id]
           ,[email]
           ,[nimimerkki]
           ,[hash]
           ,[kuva]
           ,[kuvaus]
           ,[aktiivisuus])
     VALUES
           (<kayttajataso_id, int,>
           ,<email, nvarchar(50),>
           ,<nimimerkki, nvarchar(20),>
           ,<hash, varchar(64),>
           ,<kuva, image,>
           ,<kuvaus, nvarchar(200),>
           ,<aktiivisuus, datetime,>)
GO

