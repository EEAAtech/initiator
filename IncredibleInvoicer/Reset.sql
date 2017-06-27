--SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'

EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
DELETE FROM Holidays
DELETE FROM Employees
 DELETE FROM EmpTypes
 DELETE FROM EmploymentHistory
 DELETE FROM EDocTypes
 DELETE FROM DailyAllowance
 DELETE FROM Attendance
 DELETE FROM Wages
 DELETE FROM Advance
 DELETE FROM AllowanceTypes
 DELETE FROM Allowance
 DELETE FROM Payroll
 DELETE FROM Loans
 DELETE FROM LoanSkip
 DELETE FROM Bonus
 DELETE FROM PayrollRemarks
 DELETE FROM PayrollAllowance
 DELETE FROM LoanPay
 DELETE FROM EmpDocs
 exec sp_msforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"
DBCC CHECKIDENT ( 'Holidays', RESEED, 0) 
  DBCC CHECKIDENT ( 'Employees', RESEED, 0) 
  DBCC CHECKIDENT ( 'EmpTypes', RESEED, 0) 
  DBCC CHECKIDENT ( 'EmploymentHistory', RESEED, 0) 
  DBCC CHECKIDENT ( 'EDocTypes', RESEED, 0) 
  DBCC CHECKIDENT ( 'DailyAllowance', RESEED, 0) 
  DBCC CHECKIDENT ( 'Attendance', RESEED, 0) 
  DBCC CHECKIDENT ( 'Wages', RESEED, 0) 
  DBCC CHECKIDENT ( 'Advance', RESEED, 0) 
  DBCC CHECKIDENT ( 'AllowanceTypes', RESEED, 0) 
  DBCC CHECKIDENT ( 'Allowance', RESEED, 0) 
  DBCC CHECKIDENT ( 'Payroll', RESEED, 0) 
  DBCC CHECKIDENT ( 'Loans', RESEED, 0) 
  DBCC CHECKIDENT ( 'LoanSkip', RESEED, 0) 
  DBCC CHECKIDENT ( 'Bonus', RESEED, 0) 
  DBCC CHECKIDENT ( 'PayrollRemarks', RESEED, 0) 
  DBCC CHECKIDENT ( 'PayrollAllowance', RESEED, 0) 
  DBCC CHECKIDENT ( 'LoanPay', RESEED, 0) 
  DBCC CHECKIDENT ( 'EmpDocs', RESEED, 0) 


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
