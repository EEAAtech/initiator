--SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'

--SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'

EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
DELETE FROM Customers
DELETE FROM InvoiceDetails
DELETE FROM Invoices
DELETE FROM InvYrs
DELETE FROM Items
DELETE FROM Taxes
DELETE FROM Units
exec sp_msforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"
DBCC CHECKIDENT ( 'Customers', RESEED, 0) 
DBCC CHECKIDENT ( 'InvoiceDetails', RESEED, 0) 
DBCC CHECKIDENT ( 'Invoices', RESEED, 0) 
DBCC CHECKIDENT ( 'InvYrs', RESEED, 0) 
DBCC CHECKIDENT ( 'Items', RESEED, 0) 
DBCC CHECKIDENT ( 'Taxes', RESEED, 0) 
DBCC CHECKIDENT ( 'Units', RESEED, 0) 

  INSERT INTO [dbo].[States] ([State]) VALUES ( 'Goa') 
INSERT INTO [dbo].[States] ([State]) VALUES ( 'Uttar Pradesh') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Maharashtra') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Bihar') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'West Bengal') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Madhya Pradesh') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Tamil Nadu') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Rajasthan') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Karnataka') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Gujarat') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Andhra Pradesh') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Odisha') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Telangana') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Kerala') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Jharkhand') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Assam') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Punjab') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Chhattisgarh') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Haryana') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Jammu and Kashmir') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Uttarakhand') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Himachal Pradesh') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Tripura') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Meghalaya') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Manipur') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Nagaland')  
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Arunachal Pradesh') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Mizoram') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Sikkim') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Delhi') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Puducherry') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Chandigarh') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Andaman and Nicobar Islands') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Dadra and Nagar Haveli') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Daman and Diu') 
 INSERT INTO [dbo].[States] ([State]) VALUES ( 'Lakshadweep') 
