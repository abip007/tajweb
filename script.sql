USE [MMS6]
GO
/****** Object:  View [dbo].[vwCollection]    Script Date: 1/14/2017 5:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwCollection]
AS
SELECT        dbo.RecieveMaster.rDate, dbo.RecieveDetail.Amount, dbo.Header.HeaderName
FROM            dbo.RecieveMaster INNER JOIN
                         dbo.RecieveDetail ON dbo.RecieveMaster.rid = dbo.RecieveDetail.rid INNER JOIN
                         dbo.Header ON dbo.RecieveDetail.HeaderId = dbo.Header.HeaderId

GO
/****** Object:  View [dbo].[vwReceiveable]    Script Date: 1/14/2017 5:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwReceiveable]
AS
SELECT        dbo.Header.HeaderName, dbo.Billdetail.Amount, dbo.BillRegister.BillDate, dbo.BillRegister.Paid
FROM            dbo.BillRegister INNER JOIN
                         dbo.Billdetail ON dbo.BillRegister.BillID = dbo.Billdetail.BillID INNER JOIN
                         dbo.Header ON dbo.Billdetail.HeaderId = dbo.Header.HeaderId

GO
/****** Object:  StoredProcedure [dbo].[spAllCustomerLeader]    Script Date: 1/14/2017 5:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spAllCustomerLeader]
	as
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select c.[Organization_Name], (select sum(b.Amount) as BillAmount  from [dbo].[BillRegister] a, [dbo].[Billdetail] b where a.BillId=b.BillId and c.memberId=a.MemberId) as BillAMount,(select sum(b.Amount) as CollectionAmount from [dbo].[RecieveMaster] a, [dbo].[RecieveDetail] b where a.rid=b.rid and c.memberId=a.memberId) cr  from [dbo].[MemberCompanyInfo] c where c.status='Y'
END

GO
/****** Object:  StoredProcedure [dbo].[spCollection]    Script Date: 1/14/2017 5:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[spCollection]
	@sDate date,
	@endDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  select * from vwCollection where rDate between @sDate and  @endDate
	--union select 0 as BillAmount ,sum(b.Amount) as CollectionAmount,c.HeaderName from [dbo].[RecieveMaster] a, [dbo].[RecieveDetail] b,Header c where a.rid=b.rid and c.HeaderId=b.headerid and a.rDate between @sDate and  @endDate  group by headerName 
END

GO
/****** Object:  StoredProcedure [dbo].[spMonthlyStatement]    Script Date: 1/14/2017 5:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spMonthlyStatement]
	@sDate date,
	@endDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  select c.HeaderName, (select sum(b.Amount) as BillAmount  from [dbo].[BillRegister] a, [dbo].[Billdetail] b where a.BillId=b.BillId and c.HeaderId=b.headerid and a.BillDate between @sDate and  @endDate) as BillAMount,(select sum(b.Amount) as CollectionAmount from [dbo].[RecieveMaster] a, [dbo].[RecieveDetail] b where a.rid=b.rid and c.HeaderId=b.headerid and a.rDate between @sDate and  @endDate) cr  from Header c
	--union select 0 as BillAmount ,sum(b.Amount) as CollectionAmount,c.HeaderName from [dbo].[RecieveMaster] a, [dbo].[RecieveDetail] b,Header c where a.rid=b.rid and c.HeaderId=b.headerid and a.rDate between @sDate and  @endDate  group by headerName 
END

GO
/****** Object:  StoredProcedure [dbo].[spRecieveable]    Script Date: 1/14/2017 5:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spRecieveable]
	@sDate date,
	@endDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  select * from vwReceiveable where Billdate  between @sDate and  @endDate and paid='N'
	--union select 0 as BillAmount ,sum(b.Amount) as CollectionAmount,c.HeaderName from [dbo].[RecieveMaster] a, [dbo].[RecieveDetail] b,Header c where a.rid=b.rid and c.HeaderId=b.headerid and a.rDate between @sDate and  @endDate  group by headerName 
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RecieveMaster"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RecieveDetail"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Header"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 119
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwCollection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwCollection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "BillRegister"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Billdetail"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 150
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Header"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 119
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwReceiveable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwReceiveable'
GO
