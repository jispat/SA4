/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DELETE FROM [dbo].[User]
GO
DELETE FROM [dbo].[Token]
GO
DELETE FROM [dbo].[Log]
GO
DELETE FROM [dbo].[EmailTemplate]
GO
INSERT [dbo].[EmailTemplate] ([EmailTemplateId], [TemplateName], [Subject], [Body]) VALUES (N'dd4c9780-bf50-446e-935d-375c0242144c', N'ForgotPassword', N'MYSITE - Forgot Password', N'<!DOCTYPE html>
<html>
<head>
    <title>MYSITE - Forgot Password</title>
    <meta charset="utf-8" />
</head>
<body>
    <p>
        Hello,
    </p><br />
    <p>
        Please click <a href="http://www.MYSITE.com/ResetPassword?code=##Code##">Here</a> to reset your password.
    </p><br />
    <p>
        Thanks,
        <a href="http://www.MYSITE.com">www.MYSITE.com</a><br />
        MYSITE Team
    </p><br />
</body>
</html>
')
GO
INSERT [dbo].[EmailTemplate] ([EmailTemplateId], [TemplateName], [Subject], [Body]) VALUES (N'5ed5af79-1fab-4b79-a6e9-489129f47e95', N'ChangePassword', N'MYSITE - Changed Password', N'<!DOCTYPE html>
<html>
<head>
    <title>MYSITE - Changed Password</title>
    <meta charset="utf-8" />
</head>
<body>
    <p>
        Hello,
    </p><br />
    <p>
        Your password has been changed.
    </p><br />
    <p>
        Thanks,
        <a href="http://www.MYSITE.com">www.MYSITE.com</a><br />
        MYSITE Team
    </p><br />
</body>
</html>
')
GO
INSERT [dbo].[EmailTemplate] ([EmailTemplateId], [TemplateName], [Subject], [Body]) VALUES (N'bbdd855a-52b5-4ebe-92af-86f42e265db8', N'ResetPassword', N'MYSITE - Reset Password', N'<!DOCTYPE html>
<html>
<head>
    <title>MYSITE - Reset Password</title>
    <meta charset="utf-8" />
</head>
<body>
    <p>
        Hello,
    </p><br />
    <p>
        Your password has been reset.
    </p><br />
    <p>
        Thanks,
        <a href="http://www.MYSITE.com">www.MYSITE.com</a><br />
        MYSITE Team
    </p><br />
</body>
</html>
')
GO
INSERT [dbo].[EmailTemplate] ([EmailTemplateId], [TemplateName], [Subject], [Body]) VALUES (N'aa711321-c31b-49a7-8b2c-da1441b53f92', N'UserActivation', N'MYSITE - Activate your account', N'<!DOCTYPE html>
<html>
<head>
    <title>MYSITE - Activate your account</title>
    <meta charset="utf-8" />
</head>
<body>
    <p>
        Hello,
    </p><br />
    <p>
        Thank you for joining MYSITE! Please activate your account click <a href="http://www.MYSITE.com/UserActivation?code=##Code##">Here</a>.
    </p><br />
    <p>
        Thanks,
        <a href="http://www.MYSITE.com">www.MYSITE.com</a><br />
        MYSITE Team
    </p><br />
</body>
</html>
')
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Mobile], [Email], [Password], [Code], [IsActive], [CreatedOn], [UpdatedOn]) VALUES (N'4bc802a3-7c7d-44b0-84e8-22749614eb1e', N'Admin', N'Admin ', N'1234567890', N'Admin@MuktasInvoice.com', N'e3afed0047b08059d0fada10f400c1e5', NULL, 1, CAST(N'2017-08-17T23:19:17.363' AS DateTime), NULL)
GO
