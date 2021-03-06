USE [master]
GO
/****** Object:  Database [HLAQSC]    Script Date: 2019/1/7 18:01:32 ******/
CREATE DATABASE [HLAQSC] ON  PRIMARY 
( NAME = N'HLAQSC', FILENAME = N'E:\SqlDB\MSSQL14.MSSQLSERVER\MSSQL\DATA\HLAQSC.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HLAQSC_log', FILENAME = N'E:\SqlDB\MSSQL14.MSSQLSERVER\MSSQL\DATA\HLAQSC_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HLAQSC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HLAQSC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HLAQSC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HLAQSC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HLAQSC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HLAQSC] SET ARITHABORT OFF 
GO
ALTER DATABASE [HLAQSC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HLAQSC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HLAQSC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HLAQSC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HLAQSC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HLAQSC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HLAQSC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HLAQSC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HLAQSC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HLAQSC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HLAQSC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HLAQSC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HLAQSC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HLAQSC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HLAQSC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HLAQSC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HLAQSC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HLAQSC] SET RECOVERY FULL 
GO
ALTER DATABASE [HLAQSC] SET  MULTI_USER 
GO
ALTER DATABASE [HLAQSC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HLAQSC] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'HLAQSC', N'ON'
GO
USE [HLAQSC]
GO
/****** Object:  UserDefinedFunction [dbo].[FP_EquiDataEntry_DField]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[FP_EquiDataEntry_DField]
(
	@ID varchar(100)
)
RETURNS varchar(100)
AS
BEGIN
	declare @EquiID varchar(100)
	declare @Status varchar(1)
	select @EquiID = EquipmentID, @Status = Status
	from T5_Equipment_WorkRecord
	where 1=1
		and ID = @ID

	declare @t_field table(i int, id varchar(100), unit varchar(100))

	insert into @t_field
	select
		row_number() over (order by T3_Dynamic_Field.I), T3_Dynamic_Field.FieldKey, T1_DataDirc.DircTitle
	from T3_Equipment
		left join T3_Dynamic_Field on 1=1
			and T3_Dynamic_Field.Type1 = 'Equipment'
			and T3_Equipment.Type = T3_Dynamic_Field.Type2
			and T3_Dynamic_Field.FieldKey in ('Equipment_1_1', 'Equipment_1_2', 'Equipment_2_1', 'Equipment_2_2')
		left join T1_DataDirc on 1=1
			and T1_DataDirc.Type = 'FieldUnit'
			and T3_Dynamic_Field.FieldUnit = T1_DataDirc.DircKey
	where 1=1
		and T3_Equipment.ID = @EquiID

	declare @ret numeric(20, 2)
	set @ret = 0
	declare @fieldValue numeric(20, 2)
	declare @i int
	declare @c int

	set @i = 1
	select @c = count(1) from @t_field

	while(@i <= @c)
	begin
		set @fieldValue = 0
		select @fieldValue = cast(FieldValue as numeric(20, 2))
		from T5_Equipment_WorkRecord_Field
		where 1=1
			and WorkRecordID = @ID
			and FieldKey = (
					select id
					from @t_field
					where i = @i
				)

		select @ret = @ret + @fieldValue

		set @i = @i + 1
	end

	if(@Status = '0')
	begin
		return ''
	end
	else
	begin
		return cast(@ret as varchar(100)) + ' 吨'
	end

	return ''
END
GO
/****** Object:  UserDefinedFunction [dbo].[FP_Tool_CodeAddOne]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[FP_Tool_CodeAddOne]
(
	@PCode varchar(100),
	@Code varchar(100)
)
RETURNS varchar(100)
AS
BEGIN
	declare @ret varchar(100)

	-- 0 1 2 3 4 5 6 7 8 9
	-- A B C D E F G H I J
	-- K L M N O P Q R S T
	-- U V W X Y Z
	declare @str varchar(100)
	set @str = '0123456789'
	set @str = @str + 'abcdefg'
	set @str = @str + 'hijklmn'
	set @str = @str + 'opqrst'
	set @str = @str + 'uvwxyz'
	set @str = @str + '0'

	if(@Code is null)
	begin
		set @Code = '000'
	end
	else
	begin
		set @Code = right(@Code, 3)
	end

	set @Code = lower(@Code)

	declare @a varchar(100)
	declare @b varchar(100)
	declare @c varchar(100)

	set @a = left(@Code, 1)
	set @b = right(left(@Code, 2), 1)
	set @c = right(@Code, 1)

	set @a = charindex(@a, @str) - 1
	set @b = charindex(@b, @str) - 1
	set @c = charindex(@c, @str) - 1

	declare @sum int
	set @sum = @a * 36 * 36 + @b * 36 + @c + 1

	set @a = '0'
	set @b = '0'
	set @c = '0'

	if(@sum / (36 * 36) > 0)
	begin
		set @a = @sum / (36 * 36)
		set @a = substring(@str, cast(@a as int) + 1, 1)
	end

	if((@sum % (36 * 36)) / 36 > 0)
	begin
		set @b = (@sum % (36 * 36)) / 36
		set @b = substring(@str, cast(@b as int) + 1, 1)
	end

	if(@sum % 36 > 0)
	begin
		set @c = @sum % 36
		set @c = substring(@str, cast(@c as int) + 1, 1)
	end

	return @PCode + upper(@a + @b + @c)
END
GO
/****** Object:  UserDefinedFunction [dbo].[FP_Tool_IDAddOne]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	ID追加1
-- =============================================
CREATE FUNCTION [dbo].[FP_Tool_IDAddOne]
(
	@ID varchar(100),
	@Len int
)
RETURNS varchar(100)
AS
BEGIN
	declare @ret varchar(100)
	
	declare @str1 varchar(100)
	set @str1 = '0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000'

	if(@ID is null or @ID = '')
	begin
		set @ret = right(@str1, @Len)
	end
	else
	begin
		set @ret = lower(@ID)

		declare @str2 varchar(100)
		set @str2 = ''
		set @str2 = @str2 + 'abcdefg'
		set @str2 = @str2 + 'hijklmn'
		set @str2 = @str2 + 'opqrst'
		set @str2 = @str2 + 'uvwxyz'

		declare @i int
		set @i = 1
		while(substring(@str2, @i, 1) != '')
		begin
			set @ret = replace(@ret, substring(@str2, @i, 1), '')

			set @i = @i + 1
		end
	end

	set @Len = len(@ret)
	set @ret = right(@str1 + cast(cast(@ret as numeric(30, 0)) + 1 as varchar(100)), @Len)

	return @ret
END
GO
/****** Object:  UserDefinedFunction [dbo].[FP_Tool_IDAddOne_2]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	ID追加1
-- =============================================
create FUNCTION [dbo].[FP_Tool_IDAddOne_2]
(
	@ID varchar(100),
	@Len int
)
RETURNS varchar(100)
AS
BEGIN
	declare @ret varchar(100)
	
	declare @str1 varchar(100)
	set @str1 = '0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000'

	if(@ID is null or @ID = '')
	begin
		set @ret = right(@str1, @Len)
	end
	else
	begin
		set @ret = lower(@ID)

		declare @str2 varchar(100)
		set @str2 = ''
		set @str2 = @str2 + 'abcdefg'
		set @str2 = @str2 + 'hijklmn'
		set @str2 = @str2 + 'opqrst'
		set @str2 = @str2 + 'uvwxyz'

		declare @i int
		set @i = 1
		while(substring(@str2, @i, 1) != '')
		begin
			set @ret = replace(@ret, substring(@str2, @i, 1), '')

			set @i = @i + 1
		end
	end

	--set @Len = len(@ret)
	set @ret = right(@str1 + cast(cast(@ret as numeric(30, 0)) + 1 as varchar(100)), @Len)

	return @ret
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_Report1]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FT_Report1]
(
	@Year	varchar(4),
	@Month	varchar(2)
)
RETURNS @ret TABLE 
(
	ID		varchar(100),
	Type1	varchar(100),
	Y1		int,
	Type2	varchar(100),
	D1		numeric(20,2),
	D2		numeric(20,2),
	D3		numeric(20,2),
	D4		numeric(20,2),
	D5		numeric(20,2),
	D6		numeric(20,2),
	D7		numeric(20,2),
	D8		numeric(20,2),
	D9		numeric(20,2),
	D10		numeric(20,2),
	D11		numeric(20,2),
	D12		numeric(20,2),
	D13		numeric(20,2),
	D14		numeric(20,2),
	D15		numeric(20,2),
	D16		numeric(20,2),
	D17		numeric(20,2),
	D18		numeric(20,2),
	D19		numeric(20,2),
	D20		numeric(20,2),
	D21		numeric(20,2),
	D22		numeric(20,2),
	D23		numeric(20,2),
	D24		numeric(20,2),
	D25		numeric(20,2),
	D26		numeric(20,2),
	D27		numeric(20,2),
	D28		numeric(20,2),
	D29		numeric(20,2),
	D30		numeric(20,2),
	D31		numeric(20,2),
	HJ		numeric(20,2)
)
AS
BEGIN
	declare @bday int
	set @bday = 0
	declare @eday int
	set @eday = 0

	select
		@bday = isnull(min(cast(T8_Report1.Day as int)), 0),
		@eday = isnull(max(cast(T8_Report1.Day as int)), 0)
	from T8_Report1
	where 1=1
		and T8_Report1.Year = @Year
		and T8_Report1.Month = @Month
		and T8_Report1.Type2_Code = '001001'

	if(@bday = 0 and @eday = 0)
	begin
		return
	end

	insert into @ret(
		ID, Type1, Y1, Type2, HJ
	)
	select
		T8_Report1.Type2_Code, T8_Report1.Type1_Name, T8_Report1.Y1, T8_Report1.Type2_Name, 0
	from T8_Report1
	where 1=1
		and T8_Report1.Year = @Year
		and T8_Report1.Month = @Month
		and T8_Report1.Day = right('0' + cast(@bday as varchar(100)), 2)
	order by T8_Report1.Type2_Code

	update @ret
	set
		D1 = case when 1  < @bday or @eday < 1  then null else 0 end,
		D2 = case when 2  < @bday or @eday < 2  then null else 0 end,
		D3 = case when 3  < @bday or @eday < 3  then null else 0 end,
		D4 = case when 4  < @bday or @eday < 4  then null else 0 end,
		D5 = case when 5  < @bday or @eday < 5  then null else 0 end,
		D6 = case when 6  < @bday or @eday < 6  then null else 0 end,
		D7 = case when 7  < @bday or @eday < 7  then null else 0 end,
		D8 = case when 8  < @bday or @eday < 8  then null else 0 end,
		D9 = case when 9  < @bday or @eday < 9  then null else 0 end,
		D10= case when 10 < @bday or @eday < 10 then null else 0 end,
		D11= case when 11 < @bday or @eday < 11 then null else 0 end,
		D12= case when 12 < @bday or @eday < 12 then null else 0 end,
		D13= case when 13 < @bday or @eday < 13 then null else 0 end,
		D14= case when 14 < @bday or @eday < 14 then null else 0 end,
		D15= case when 15 < @bday or @eday < 15 then null else 0 end,
		D16= case when 16 < @bday or @eday < 16 then null else 0 end,
		D17= case when 17 < @bday or @eday < 17 then null else 0 end,
		D18= case when 18 < @bday or @eday < 18 then null else 0 end,
		D19= case when 19 < @bday or @eday < 19 then null else 0 end,
		D20= case when 20 < @bday or @eday < 20 then null else 0 end,
		D21= case when 21 < @bday or @eday < 21 then null else 0 end,
		D22= case when 22 < @bday or @eday < 22 then null else 0 end,
		D23= case when 23 < @bday or @eday < 23 then null else 0 end,
		D24= case when 24 < @bday or @eday < 24 then null else 0 end,
		D25= case when 25 < @bday or @eday < 25 then null else 0 end,
		D26= case when 26 < @bday or @eday < 26 then null else 0 end,
		D27= case when 27 < @bday or @eday < 27 then null else 0 end,
		D28= case when 28 < @bday or @eday < 28 then null else 0 end,
		D29= case when 29 < @bday or @eday < 29 then null else 0 end,
		D30= case when 30 < @bday or @eday < 30 then null else 0 end,
		D31= case when 31 < @bday or @eday < 31 then null else 0 end

	declare @i int
	set @i = @bday

	while(@i <= @eday)
	begin
		update @ret
		set
			D1 = case when @i = 1  then T8_Report1.Val else D1  end,
			D2 = case when @i = 2  then T8_Report1.Val else D2  end,
			D3 = case when @i = 3  then T8_Report1.Val else D3  end,
			D4 = case when @i = 4  then T8_Report1.Val else D4  end,
			D5 = case when @i = 5  then T8_Report1.Val else D5  end,
			D6 = case when @i = 6  then T8_Report1.Val else D6  end,
			D7 = case when @i = 7  then T8_Report1.Val else D7  end,
			D8 = case when @i = 8  then T8_Report1.Val else D8  end,
			D9 = case when @i = 9  then T8_Report1.Val else D9  end,
			D10= case when @i = 10 then T8_Report1.Val else D10 end,
			D11= case when @i = 11 then T8_Report1.Val else D11 end,
			D12= case when @i = 12 then T8_Report1.Val else D12 end,
			D13= case when @i = 13 then T8_Report1.Val else D13 end,
			D14= case when @i = 14 then T8_Report1.Val else D14 end,
			D15= case when @i = 15 then T8_Report1.Val else D15 end,
			D16= case when @i = 16 then T8_Report1.Val else D16 end,
			D17= case when @i = 17 then T8_Report1.Val else D17 end,
			D18= case when @i = 18 then T8_Report1.Val else D18 end,
			D19= case when @i = 19 then T8_Report1.Val else D19 end,
			D20= case when @i = 20 then T8_Report1.Val else D20 end,
			D21= case when @i = 21 then T8_Report1.Val else D21 end,
			D22= case when @i = 22 then T8_Report1.Val else D22 end,
			D23= case when @i = 23 then T8_Report1.Val else D23 end,
			D24= case when @i = 24 then T8_Report1.Val else D24 end,
			D25= case when @i = 25 then T8_Report1.Val else D25 end,
			D26= case when @i = 26 then T8_Report1.Val else D26 end,
			D27= case when @i = 27 then T8_Report1.Val else D27 end,
			D28= case when @i = 28 then T8_Report1.Val else D28 end,
			D29= case when @i = 29 then T8_Report1.Val else D29 end,
			D30= case when @i = 30 then T8_Report1.Val else D30 end,
			D31= case when @i = 31 then T8_Report1.Val else D31 end
		from T8_Report1
		where 1=1
			and T8_Report1.Year = @Year
			and T8_Report1.Month = @Month
			and T8_Report1.Day = right('0' + cast(@i as varchar(100)), 2)
			and [@ret].ID = T8_Report1.Type2_Code

		set @i = @i + 1
	end

	update @ret
	set HJ = 
		isnull(D1, 0) +
		isnull(D2, 0) +
		isnull(D3, 0) +
		isnull(D4, 0) +
		isnull(D5, 0) +
		isnull(D6, 0) +
		isnull(D7, 0) +
		isnull(D8, 0) +
		isnull(D9, 0) +
		isnull(D10, 0) +
		isnull(D11, 0) +
		isnull(D12, 0) +
		isnull(D13, 0) +
		isnull(D14, 0) +
		isnull(D15, 0) +
		isnull(D16, 0) +
		isnull(D17, 0) +
		isnull(D18, 0) +
		isnull(D19, 0) +
		isnull(D20, 0) +
		isnull(D21, 0) +
		isnull(D22, 0) +
		isnull(D23, 0) +
		isnull(D24, 0) +
		isnull(D25, 0) +
		isnull(D26, 0) +
		isnull(D27, 0) +
		isnull(D28, 0) +
		isnull(D29, 0) +
		isnull(D30, 0) +
		isnull(D31, 0)



	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_SCJ_Info_CC_ByLoginName]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FT_SCJ_Info_CC_ByLoginName]
(
	@LoginName varchar(100),
	@ZDCode varchar(100),
	@CCCode varchar(100)
)
RETURNS @ret TABLE 
(
	positionCode varchar(100),
	col1 varchar(100),
	col2 varchar(100)
)
AS
BEGIN
	declare @month varchar(100)
	set @month = cast(year(getdate()) as varchar(100)) + '-' + right('0' + cast(month(getdate()) as varchar(100)), 2)

	if(@ZDCode is null)
	begin
		set @ZDCode = ''
	end

	if(@CCCode is null)
	begin
		set @CCCode = ''
	end

	declare @t_zd table(i int, code varchar(100), title varchar(100))
	insert into @t_zd
	select
		row_number() over (order by Code), Code, Title
	from dbo.FT_SCJ_Position_ByLoginName(@LoginName, '2')
	where 1=1
		and (@ZDCode = '' or Code like @ZDCode + '%')
		and (@CCCode = '' or Code = @CCCode)

	declare @i int
	declare @c int
	declare @positionName varchar(100)
	set @i = 1
	select @c = count(1) from @t_zd

	declare @title varchar(100)
	declare @key varchar(100)

	while(@i <= @c)
	begin
		select @CCCode = code, @positionName = title from @t_zd where i = @i

		insert into @ret
		select
			@CCCode, '作业面总数', count(1)
		from T4_MP
			left join T2_Position on T4_MP.PositionCode = T2_Position.Code
		where 1=1
			and T4_MP.Month = @month
			and T4_MP.PositionCode like @CCCode + '%'
			and T2_Position.Type = '3'

		insert into @ret
		select
			@CCCode, T1_DataDirc.DircTitle, 
			(
				select count(1)
				from T4_MP
					left join T2_Position on T4_MP.PositionCode = T2_Position.Code
				where 1=1
					and T4_MP.Month = @month
					and T4_MP.PositionCode like @CCCode + '%'
					and T2_Position.Type = '3'
					and T4_MP.Status = T1_DataDirc.DircKey
			)
		from 
			T1_DataDirc
		where 1=1
			and T1_DataDirc.Type = 'ZYMStatus'

		set @i = @i + 1
	end

	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_SCJ_Info_ZD_ByLoginName]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FT_SCJ_Info_ZD_ByLoginName]
(
	@LoginName varchar(100),
	@ZDCode varchar(100)
)
RETURNS @ret TABLE 
(
	positionCode varchar(100),
	col1 varchar(100),
	col2 varchar(100)
)
AS
BEGIN
	declare @month varchar(100)
	set @month = cast(year(getdate()) as varchar(100)) + '-' + right('0' + cast(month(getdate()) as varchar(100)), 2)

	if(@ZDCode is null)
	begin
		set @ZDCode = ''
	end

	declare @t_zd table(i int, code varchar(100), title varchar(100))
	insert into @t_zd
	select
		row_number() over (order by Code), Code, Title
	from dbo.FT_SCJ_Position_ByLoginName(@LoginName, '1')
	where 1=1
		and (@ZDCode = '' or Code = @ZDCode)

	declare @i int
	declare @c int
	declare @positionName varchar(100)
	set @i = 1
	select @c = count(1) from @t_zd

	declare @title varchar(1000)
	declare @key varchar(100)

	while(@i <= @c)
	begin
		select @ZDCode = code, @positionName = title from @t_zd where i = @i

		insert into @ret
		select
			@ZDCode, '采场总数', count(1)
		from T4_MP
			left join T2_Position on T4_MP.PositionCode = T2_Position.Code
		where 1=1
			and T4_MP.Month = @month
			and T4_MP.PositionCode like @ZDCode + '%'
			and T2_Position.Type = '2'

		set @i = @i + 1
	end

	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_SCJ_Info_ZYM_ByLoginName]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FT_SCJ_Info_ZYM_ByLoginName]
(
	@LoginName varchar(100),
	@ZDCode varchar(100),
	@CCCode varchar(100),
	@ZYMCode varchar(100)
)
RETURNS @ret TABLE 
(
	positionCode varchar(100),
	col1 varchar(100),
	col2 varchar(100)
)
AS
BEGIN
	declare @month varchar(100)
	set @month = cast(year(getdate()) as varchar(100)) + '-' + right('0' + cast(month(getdate()) as varchar(100)), 2)

	if(@ZDCode is null)
	begin
		set @ZDCode = ''
	end

	if(@CCCode is null)
	begin
		set @CCCode = ''
	end

	if(@ZYMCode is null)
	begin
		set @ZYMCode = ''
	end

	declare @t_zd table(i int, code varchar(100), title varchar(100))
	insert into @t_zd
	select
		row_number() over (order by Code), Code, Title
	from dbo.FT_SCJ_Position_ByLoginName(@LoginName, '3')
	where 1=1
		and (@ZDCode = '' or Code like @ZDCode + '%')
		and (@CCCode = '' or Code like @CCCode + '%')
		and (@ZYMCode = '' or Code = @ZYMCode)

	declare @i int
	declare @c int
	declare @positionName varchar(100)
	set @i = 1
	select @c = count(1) from @t_zd

	declare @title varchar(100)
	declare @key varchar(100)

	while(@i <= @c)
	begin
		select @ZYMCode = code, @positionName = title from @t_zd where i = @i

		insert into @ret
		select
			@ZYMCode, '作业面状态', isnull(T4_MP.Status, '')
		from T4_MP
			--left join T1_DataDirc on T1_DataDirc.Type = 'ZYMStatus' and T4_MP.Status = T1_DataDirc.DircKey
		where 1=1
			and T4_MP.Month = @month
			and T4_MP.PositionCode = @ZYMCode

		set @i = @i + 1
	end

	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_SCJ_MonthInfo_CC_ByLoginName]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FT_SCJ_MonthInfo_CC_ByLoginName]
(
	@LoginName varchar(100),
	@ZDCode varchar(100),
	@CCCode varchar(100)
)
RETURNS @ret TABLE 
(
	positionCode varchar(100),
	col1 varchar(100),
	col2 varchar(100),
	col3 varchar(100),
	col4 varchar(100)
)
AS
BEGIN
	declare @month varchar(100)
	set @month = cast(year(getdate()) as varchar(100)) + '-' + right('0' + cast(month(getdate()) as varchar(100)), 2)

	if(@ZDCode is null)
	begin
		set @ZDCode = ''
	end

	if(@CCCode is null)
	begin
		set @CCCode = ''
	end

	declare @t_zd table(i int, code varchar(100), title varchar(100))
	insert into @t_zd
	select
		row_number() over (order by Code), Code, Title
	from dbo.FT_SCJ_Position_ByLoginName(@LoginName, '2')
	where 1=1
		and (@ZDCode = '' or Code like @ZDCode + '%')
		and (@CCCode = '' or Code = @CCCode)

	declare @i int
	declare @c int
	declare @positionName varchar(100)
	set @i = 1
	select @c = count(1) from @t_zd

	declare @title varchar(100)
	declare @key varchar(100)

	while(@i <= @c)
	begin
		select @CCCode = code, @positionName = title from @t_zd where i = @i

		set @title = '放矿'
		insert into @ret
		select
			@CCCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and ConfigCode like 'B1_1__'

		set @title = '提升'
		insert into @ret
		select
			@CCCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and ConfigCode like 'B1_2__'

		set @title = '掘进 - ' + @positionName
		set @key = 'B4_05'
		
		insert into @ret
		select
			@CCCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and PositionCode like @CCCode + '%'
			and ConfigCode = @key

		set @title = '供矿 - ' + @positionName
		set @key = 'B7_09'
		
		insert into @ret
		select
			@CCCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' T', '0.000 T', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and PositionCode like @CCCode + '%'
			and ConfigCode = @key

		set @i = @i + 1
	end

	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_SCJ_MonthInfo_ZD_ByLoginName]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FT_SCJ_MonthInfo_ZD_ByLoginName]
(
	@LoginName varchar(100),
	@ZDCode varchar(100)
)
RETURNS @ret TABLE 
(
	positionCode varchar(100),
	col1 varchar(100),
	col2 varchar(100),
	col3 varchar(100),
	col4 varchar(100)
)
AS
BEGIN
	declare @month varchar(100)
	set @month = cast(year(getdate()) as varchar(100)) + '-' + right('0' + cast(month(getdate()) as varchar(100)), 2)

	if(@ZDCode is null)
	begin
		set @ZDCode = ''
	end

	declare @t_zd table(i int, code varchar(100), title varchar(100))
	insert into @t_zd
	select
		row_number() over (order by Code), Code, Title
	from dbo.FT_SCJ_Position_ByLoginName(@LoginName, '1')
	where 1=1
		and (@ZDCode = '' or Code = @ZDCode)

	declare @i int
	declare @c int
	declare @positionName varchar(100)
	set @i = 1
	select @c = count(1) from @t_zd

	declare @title varchar(1000)
	declare @key varchar(100)

	while(@i <= @c)
	begin
		select @ZDCode = code, @positionName = title from @t_zd where i = @i

		set @title = '放矿'
		insert into @ret
		select
			@ZDCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and ConfigCode like 'B1_1__'

		set @title = '提升'
		insert into @ret
		select
			@ZDCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and ConfigCode like 'B1_2__'

		set @title = '掘进 - ' + @positionName
		set @key = 'B4_05'
		
		insert into @ret
		select
			@ZDCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and PositionCode like @ZDCode + '%'
			and ConfigCode = @key

		set @title = '供矿 - ' + @positionName
		set @key = 'B7_09'
		
		insert into @ret
		select
			@ZDCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' T', '0.000 T', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and PositionCode like @ZDCode + '%'
			and ConfigCode = @key

		set @i = @i + 1
	end

	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_SCJ_MonthInfo_ZYM_ByLoginName]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FT_SCJ_MonthInfo_ZYM_ByLoginName]
(
	@LoginName varchar(100),
	@ZDCode varchar(100),
	@CCCode varchar(100),
	@ZYMCode varchar(100)
)
RETURNS @ret TABLE 
(
	positionCode varchar(100),
	col1 varchar(100),
	col2 varchar(100),
	col3 varchar(100),
	col4 varchar(100)
)
AS
BEGIN
	declare @month varchar(100)
	set @month = cast(year(getdate()) as varchar(100)) + '-' + right('0' + cast(month(getdate()) as varchar(100)), 2)

	if(@ZDCode is null)
	begin
		set @ZDCode = ''
	end

	if(@CCCode is null)
	begin
		set @CCCode = ''
	end

	if(@ZYMCode is null)
	begin
		set @ZYMCode = ''
	end

	declare @t_zd table(i int, code varchar(100), title varchar(100))
	insert into @t_zd
	select
		row_number() over (order by Code), Code, Title
	from dbo.FT_SCJ_Position_ByLoginName(@LoginName, '3')
	where 1=1
		and (@ZDCode = '' or Code like @ZDCode + '%')
		and (@CCCode = '' or Code like @CCCode + '%')
		and (@ZYMCode = '' or Code = @ZYMCode)

	declare @i int
	declare @c int
	declare @positionName varchar(100)
	set @i = 1
	select @c = count(1) from @t_zd

	declare @title varchar(100)
	declare @key varchar(100)

	while(@i <= @c)
	begin
		select @ZYMCode = code, @positionName = title from @t_zd where i = @i

		set @title = '放矿'
		insert into @ret
		select
			@ZYMCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and ConfigCode like 'B1_1__'

		set @title = '提升'
		insert into @ret
		select
			@ZYMCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and ConfigCode like 'B1_2__'

		set @title = '掘进 - ' + @positionName
		set @key = 'B4_05'
		
		insert into @ret
		select
			@ZYMCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' M', '0.000 M', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and PositionCode = @ZYMCode
			and ConfigCode = @key

		select @ZYMCode = Code, @positionName = Title
		from T2_Position
		where 1=1
			and Code = left(@ZYMCode, len(@ZYMCode) - 3)

		set @title = '供矿 - ' + @positionName
		set @key = 'B7_09'
		
		insert into @ret
		select
			@ZYMCode, @title, cast(isnull(sum(cast(Val as numeric(20, 3))), 0.000) as varchar(100)) + ' T', '0.000 T', '0.000%' 
		from T4_MP_Detail_1
		where 1=1
			and Month = @month
			and PositionCode = @ZYMCode
			and ConfigCode = @key

		set @i = @i + 1
	end

	RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FT_SCJ_Position_ByLoginName]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	根据登录名获取有权限的位置
-- =============================================
CREATE FUNCTION [dbo].[FT_SCJ_Position_ByLoginName]
(
	@LoginName varchar(100),
	@Type varchar(100) -- 0 全部/1 中段/2 采场/3 作业面
)
RETURNS @ret TABLE 
(
	ID varchar(100),
	Code varchar(100),
	Title varchar(100)
)
AS
BEGIN
	declare @OrgCode varchar(100)
	select
		@OrgCode = OrgCode
	from T1_User
	where 1=1
		and LoginName = @LoginName

	-- 找不到对应的组织结构
	if(@OrgCode is null)
	begin
		insert into @ret
		select
			ID, Code, Title
		from T2_Position
		where 1=1
			and Del = '0'
			and (@Type = '0' or (Type = @Type))
		return
	end
	else
	begin
		insert into @ret
		select
			T2_Position.ID, T2_Position.Code, T2_Position.Title
		from T2_Position_Org
			left join T2_Position on 1=1
		where 1=1
			and T2_Position_Org.OrgCode like @OrgCode + '%'
			and T2_Position.Del = '0'
			and (@Type = '0' or (T2_Position.Type = @Type))
			and T2_Position.Code like T2_Position_Org.PositionCode + '%'
	end

	RETURN 
END
GO
/****** Object:  Table [dbo].[T0_Log]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T0_Log](
	[ID] [varchar](50) NOT NULL,
	[Txt] [varchar](8000) NULL,
 CONSTRAINT [PK_T0_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T1_DataDirc]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T1_DataDirc](
	[ID] [varchar](50) NOT NULL,
	[Type] [varchar](50) NULL,
	[DircKey] [varchar](50) NULL,
	[DircTitle] [varchar](50) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T1_DataDirc] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T1_Page]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T1_Page](
	[Code] [varchar](50) NOT NULL,
	[Type] [varchar](1) NULL,
	[OrderBy] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[Url] [varchar](100) NULL,
	[Del] [varchar](1) NULL,
 CONSTRAINT [PK_T1_Page] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T1_User]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T1_User](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[LoginName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[OrgCode] [varchar](60) NULL,
	[PRoleID] [varchar](50) NULL,
	[RRoleCode] [varchar](60) NULL,
	[DRoleType] [varchar](1) NULL,
	[JobCode] [varchar](10) NULL,
	[UserKey] [varchar](50) NULL,
	[Del] [varchar](1) NULL,
 CONSTRAINT [PK_T1_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T1_User_Admin]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T1_User_Admin](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[LoginName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_T1_User_Admin] PRIMARY KEY CLUSTERED 
(
	[LoginName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T1_User_Excel]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T1_User_Excel](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[LoginName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[OrgCode] [varchar](60) NULL,
	[PRoleID] [varchar](50) NULL,
	[RRoleCode] [varchar](60) NULL,
	[DRoleType] [varchar](1) NULL,
	[JobCode] [varchar](10) NULL,
	[UserKey] [varchar](50) NULL,
	[Del] [varchar](1) NULL,
 CONSTRAINT [PK_T1_User_Excel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_DRole]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_DRole](
	[ID] [varchar](50) NOT NULL,
	[Title] [varchar](50) NULL,
	[Type] [varchar](1) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T2_DRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_Org]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_Org](
	[ID] [varchar](50) NOT NULL,
	[Code] [varchar](60) NULL,
	[Type] [varchar](1) NULL,
	[Title] [varchar](100) NULL,
	[STitle] [varchar](100) NULL,
	[Remark] [varchar](8000) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T2_Org] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_Position]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_Position](
	[ID] [varchar](50) NOT NULL,
	[Code] [varchar](60) NULL,
	[Type] [varchar](1) NULL,
	[Title] [varchar](100) NULL,
	[Remark] [varchar](1000) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T2_Position] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_Position_Org]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_Position_Org](
	[PositionCode] [varchar](60) NOT NULL,
	[OrgCode] [varchar](60) NOT NULL,
 CONSTRAINT [PK_T2_Position_Org_1] PRIMARY KEY CLUSTERED 
(
	[PositionCode] ASC,
	[OrgCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_PRole]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_PRole](
	[ID] [varchar](50) NOT NULL,
	[Title] [varchar](50) NULL,
	[Remark] [varchar](1000) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T2_PRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_PRole_Detail]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_PRole_Detail](
	[PRoleID] [varchar](50) NOT NULL,
	[PageCode] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T2_PRole_Detail] PRIMARY KEY CLUSTERED 
(
	[PRoleID] ASC,
	[PageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_RRole]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_RRole](
	[ID] [varchar](50) NOT NULL,
	[Code] [varchar](60) NULL,
	[Type] [varchar](1) NULL,
	[Title] [varchar](100) NULL,
	[Remark] [varchar](1000) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T2_RRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_RRole_OperLog]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_RRole_OperLog](
	[ID] [varchar](50) NOT NULL,
	[WorkRecordID] [varchar](50) NULL,
	[RRoleCode] [varchar](60) NULL,
	[UserID] [varchar](50) NULL,
	[Result] [varchar](1) NULL,
	[Remark] [varchar](100) NULL,
	[RDate] [datetime] NULL,
 CONSTRAINT [PK_T2_RRole_OperLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T2_RRole_User]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T2_RRole_User](
	[RRoleID] [varchar](50) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T2_RRole_User] PRIMARY KEY CLUSTERED 
(
	[RRoleID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T3_Dynamic_Field]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T3_Dynamic_Field](
	[ID] [varchar](50) NOT NULL,
	[Type1] [varchar](50) NULL,
	[Type2] [varchar](50) NULL,
	[I] [int] NULL,
	[FieldKey] [varchar](50) NULL,
	[FieldName] [varchar](50) NULL,
	[FieldUnit] [varchar](50) NULL,
	[FieldType] [varchar](2) NULL,
	[FieldMode] [varchar](1) NULL,
 CONSTRAINT [PK_T3_Dynamic_Index] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T3_Dynamic_FieldType]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T3_Dynamic_FieldType](
	[DFKey] [varchar](50) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Title] [varchar](50) NULL,
 CONSTRAINT [PK_T3_Dynamic_FieldType] PRIMARY KEY CLUSTERED 
(
	[DFKey] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T3_Dynamic_FieldUnit]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T3_Dynamic_FieldUnit](
	[DFKey] [varchar](50) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Title] [varchar](50) NULL,
	[Unit_0_Rate] [numeric](20, 4) NULL,
 CONSTRAINT [PK_T3_Dynamic_FieldUnit] PRIMARY KEY CLUSTERED 
(
	[DFKey] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T3_Equipment]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T3_Equipment](
	[ID] [varchar](50) NOT NULL,
	[Title] [varchar](50) NULL,
	[Type] [varchar](1) NULL,
	[Remark] [varchar](50) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T3_Equipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T3_Equipment_Position]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T3_Equipment_Position](
	[EquipmentID] [varchar](50) NOT NULL,
	[PositionCode] [varchar](60) NOT NULL,
 CONSTRAINT [PK_T3_Equipment_Position] PRIMARY KEY CLUSTERED 
(
	[EquipmentID] ASC,
	[PositionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T3_EquipmentType]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T3_EquipmentType](
	[ID] [varchar](50) NOT NULL,
	[Title] [varchar](50) NULL,
	[Type] [varchar](1) NULL,
	[Del] [varchar](1) NULL,
	[Lock] [varchar](1) NULL,
 CONSTRAINT [PK_T3_EquipmentType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T3_Job]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T3_Job](
	[ID] [varchar](50) NOT NULL,
	[Code] [varchar](10) NULL,
	[Title] [varchar](100) NULL,
	[Del] [varchar](1) NULL,
 CONSTRAINT [PK_T1_Job] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T4_Config]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T4_Config](
	[Code] [varchar](50) NOT NULL,
	[Remark1] [varchar](50) NULL,
	[Unit1] [varchar](50) NULL,
	[Type1] [varchar](10) NULL,
	[Type2] [varchar](10) NULL,
	[DFKey] [varchar](50) NULL,
 CONSTRAINT [PK_T4_Config_1] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T4_MP]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T4_MP](
	[Month] [varchar](7) NOT NULL,
	[PositionCode] [varchar](60) NOT NULL,
	[WorkDayCount] [int] NULL,
	[Status] [varchar](2) NULL,
	[StatusChangeDate] [datetime] NULL,
 CONSTRAINT [PK_T4_MP] PRIMARY KEY CLUSTERED 
(
	[Month] ASC,
	[PositionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T4_MP_Detail_1]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T4_MP_Detail_1](
	[Month] [varchar](7) NOT NULL,
	[PositionCode] [varchar](60) NOT NULL,
	[ConfigCode] [varchar](50) NOT NULL,
	[Val] [varchar](50) NULL,
 CONSTRAINT [PK_T4_MP_Detail_1] PRIMARY KEY CLUSTERED 
(
	[Month] ASC,
	[PositionCode] ASC,
	[ConfigCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T4_MP_Detail_2]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T4_MP_Detail_2](
	[Month] [varchar](7) NOT NULL,
	[PositionCode] [varchar](60) NOT NULL,
	[ConfigCode] [varchar](50) NOT NULL,
	[Val] [varchar](50) NULL,
 CONSTRAINT [PK_T4_MP_Detail_2] PRIMARY KEY CLUSTERED 
(
	[Month] ASC,
	[PositionCode] ASC,
	[ConfigCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T5_MessageBoard]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T5_MessageBoard](
	[ID] [varchar](50) NOT NULL,
	[PositionCode] [varchar](60) NULL,
	[UserID] [varchar](50) NULL,
	[Remark] [varchar](100) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_T5_MessageBoard] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T5_WorkRecord]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T5_WorkRecord](
	[ID] [varchar](50) NOT NULL,
	[RecordType] [varchar](1) NULL,
	[WorkDate] [varchar](10) NULL,
	[WorkClassCode] [varchar](1) NULL,
	[WorkClassName] [varchar](50) NULL,
	[WorkHour] [numeric](3, 1) NULL,
	[WorkManID] [varchar](50) NULL,
	[WorkManName] [varchar](50) NULL,
	[RRoleCode] [varchar](60) NULL,
	[RRoleCode_Cur] [varchar](60) NULL,
	[Status] [varchar](1) NULL,
	[Del] [varchar](1) NULL,
	[DF1] [varchar](1000) NULL,
	[DF2] [varchar](50) NULL,
	[DF3] [varchar](50) NULL,
 CONSTRAINT [PK_T5_WorkRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T5_WorkRecord_Detail]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T5_WorkRecord_Detail](
	[ID] [varchar](50) NOT NULL,
	[WorkRecordID] [varchar](50) NULL,
	[EquipmentID] [varchar](50) NULL,
	[PositionCode] [varchar](60) NULL,
	[WorkHour] [numeric](3, 1) NULL,
	[WhereAbout] [varchar](50) NULL,
	[DF1] [varchar](1000) NULL,
	[DF2] [varchar](50) NULL,
	[DF3] [varchar](50) NULL,
 CONSTRAINT [PK_T5_WorkRecord_Detail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T5_WorkRecord_Detail_Field]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T5_WorkRecord_Detail_Field](
	[ID] [varchar](50) NOT NULL,
	[WorkRecordDetailID] [varchar](50) NOT NULL,
	[FieldKey] [varchar](50) NULL,
	[FieldType] [varchar](50) NULL,
	[FieldValue] [varchar](50) NULL,
	[FieldUnit] [varchar](50) NULL,
	[FieldValue0] [varchar](50) NULL,
	[FieldUnit0] [varchar](50) NULL,
 CONSTRAINT [PK_T5_WorkRecord_Detail_Field] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Check]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Check](
	[ID] [varchar](50) NOT NULL,
	[YM] [varchar](50) NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[FileNameS] [varchar](50) NOT NULL,
	[UploadTime] [datetime] NOT NULL,
 CONSTRAINT [PK_T6_Check] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Check_B1_ZongBiao]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Check_B1_ZongBiao](
	[ID] [varchar](50) NOT NULL,
	[CID] [varchar](50) NOT NULL,
	[ZB1] [varchar](50) NOT NULL,
	[ZB2] [varchar](50) NOT NULL,
	[DW] [varchar](50) NOT NULL,
	[BYYS] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Check_B1_ZongBiao] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Check_B3_CaiJue]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Check_B3_CaiJue](
	[ID] [varchar](50) NOT NULL,
	[CID] [varchar](50) NOT NULL,
	[ZB1] [varchar](50) NOT NULL,
	[ZB2] [varchar](50) NOT NULL,
	[DW] [varchar](50) NOT NULL,
	[NDYS] [varchar](50) NOT NULL,
	[YJWC1] [varchar](50) NOT NULL,
	[WCL1] [varchar](50) NOT NULL,
	[BYYS] [varchar](50) NOT NULL,
	[YJWC2] [varchar](50) NOT NULL,
	[WCL2] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Check_B3_CaiJue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Check_B4_JueJin]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Check_B4_JueJin](
	[ID] [varchar](50) NOT NULL,
	[CID] [varchar](50) NOT NULL,
	[ZD] [varchar](50) NOT NULL,
	[CC] [varchar](50) NULL,
	[ZYM] [varchar](50) NOT NULL,
	[GCLX] [varchar](50) NOT NULL,
	[TX] [varchar](50) NOT NULL,
	[TB] [varchar](50) NOT NULL,
	[GG] [varchar](50) NOT NULL,
	[DMJ] [varchar](50) NOT NULL,
	[CD] [varchar](50) NOT NULL,
	[TJ] [varchar](50) NOT NULL,
	[JJL] [varchar](50) NOT NULL,
	[ZHBM] [varchar](50) NOT NULL,
	[FC] [varchar](50) NOT NULL,
	[SGSJ] [varchar](50) NOT NULL,
	[JT] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Check_B4_JueJin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Check_B6_CaiKuang]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Check_B6_CaiKuang](
	[ID] [varchar](50) NOT NULL,
	[CID] [varchar](50) NOT NULL,
	[ZD] [varchar](50) NOT NULL,
	[CC] [varchar](50) NOT NULL,
	[CKLX] [varchar](50) NOT NULL,
	[DZPW_X] [varchar](50) NOT NULL,
	[DZPW_T] [varchar](50) NOT NULL,
	[DZPW_C] [varchar](50) NOT NULL,
	[DZPW_L] [varchar](50) NOT NULL,
	[CKL] [varchar](50) NOT NULL,
	[TCZL] [varchar](50) NOT NULL,
	[WSL] [varchar](50) NOT NULL,
	[JJL] [varchar](50) NOT NULL,
	[KSSJ] [varchar](50) NOT NULL,
	[JSSJ] [varchar](50) NOT NULL,
	[BZ] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Check_B6_CaiKuang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Check_B7_ChuKuang]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Check_B7_ChuKuang](
	[ID] [varchar](50) NOT NULL,
	[CID] [varchar](50) NOT NULL,
	[ZD] [varchar](50) NOT NULL,
	[CC] [varchar](50) NOT NULL,
	[XHKL] [varchar](50) NOT NULL,
	[DZPW_X] [varchar](50) NOT NULL,
	[DZPW_T] [varchar](50) NOT NULL,
	[DZPW_C] [varchar](50) NOT NULL,
	[DZPW_L] [varchar](50) NOT NULL,
	[PHL] [varchar](50) NOT NULL,
	[SSL] [varchar](50) NOT NULL,
	[CKPW_X] [varchar](50) NOT NULL,
	[CKPW_T] [varchar](50) NOT NULL,
	[CKL] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Check_B7_ChuKuang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Plan]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Plan](
	[ID] [varchar](50) NOT NULL,
	[YM] [varchar](50) NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[FileNameS] [varchar](50) NOT NULL,
	[UploadTime] [datetime] NOT NULL,
 CONSTRAINT [PK_T6_Plan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Plan_B1_ZongBiao]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Plan_B1_ZongBiao](
	[ID] [varchar](50) NOT NULL,
	[PID] [varchar](50) NOT NULL,
	[ZB1] [varchar](50) NOT NULL,
	[ZB2] [varchar](50) NOT NULL,
	[DW] [varchar](50) NOT NULL,
	[BYJH] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Plan_B1_ZongBiao] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Plan_B3_CaiJue]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Plan_B3_CaiJue](
	[ID] [varchar](50) NOT NULL,
	[PID] [varchar](50) NOT NULL,
	[ZB1] [varchar](50) NOT NULL,
	[ZB2] [varchar](50) NOT NULL,
	[DW] [varchar](50) NOT NULL,
	[NDJH] [varchar](50) NOT NULL,
	[YJWC1] [varchar](50) NOT NULL,
	[WCL1] [varchar](50) NOT NULL,
	[BYJH] [varchar](50) NOT NULL,
	[YJWC2] [varchar](50) NOT NULL,
	[WCL2] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Plan_B3_CaiJue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Plan_B4_JueJin]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Plan_B4_JueJin](
	[ID] [varchar](50) NOT NULL,
	[PID] [varchar](50) NOT NULL,
	[ZD] [varchar](50) NOT NULL,
	[CC] [varchar](50) NULL,
	[ZYM] [varchar](50) NOT NULL,
	[GCLX] [varchar](50) NOT NULL,
	[TX] [varchar](50) NOT NULL,
	[TB] [varchar](50) NOT NULL,
	[GG] [varchar](50) NOT NULL,
	[DMJ] [varchar](50) NOT NULL,
	[CD] [varchar](50) NOT NULL,
	[TJ] [varchar](50) NOT NULL,
	[JJL] [varchar](50) NOT NULL,
	[ZHBM] [varchar](50) NOT NULL,
	[FC] [varchar](50) NOT NULL,
	[SGSJ] [varchar](50) NOT NULL,
	[JT] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Plan_B4_JueJin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Plan_B6_CaiKuang]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Plan_B6_CaiKuang](
	[ID] [varchar](50) NOT NULL,
	[PID] [varchar](50) NOT NULL,
	[ZD] [varchar](50) NOT NULL,
	[CC] [varchar](50) NOT NULL,
	[CKLX] [varchar](50) NOT NULL,
	[DZPW_X] [varchar](50) NOT NULL,
	[DZPW_T] [varchar](50) NOT NULL,
	[DZPW_C] [varchar](50) NOT NULL,
	[DZPW_L] [varchar](50) NOT NULL,
	[CKL] [varchar](50) NOT NULL,
	[TCZL] [varchar](50) NOT NULL,
	[WSL] [varchar](50) NOT NULL,
	[JJL] [varchar](50) NOT NULL,
	[KSSJ] [varchar](50) NOT NULL,
	[JSSJ] [varchar](50) NOT NULL,
	[BZ] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Plan_B6_CaiKuang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T6_Plan_B7_ChuKuang]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T6_Plan_B7_ChuKuang](
	[ID] [varchar](50) NOT NULL,
	[PID] [varchar](50) NOT NULL,
	[ZD] [varchar](50) NOT NULL,
	[CC] [varchar](50) NOT NULL,
	[XHKL] [varchar](50) NOT NULL,
	[DZPW_X] [varchar](50) NOT NULL,
	[DZPW_T] [varchar](50) NOT NULL,
	[DZPW_C] [varchar](50) NOT NULL,
	[DZPW_L] [varchar](50) NOT NULL,
	[PHL] [varchar](50) NOT NULL,
	[SSL] [varchar](50) NOT NULL,
	[CKPW_X] [varchar](50) NOT NULL,
	[CKPW_T] [varchar](50) NOT NULL,
	[CKL] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T6_Plan_B7_ChuKuang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T7_Sample]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T7_Sample](
	[ID] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[STime] [datetime] NOT NULL,
	[PID] [varchar](50) NOT NULL,
	[Sampler] [varchar](50) NOT NULL,
	[Memo] [varchar](500) NULL,
	[Result] [varchar](500) NULL,
	[RTime] [datetime] NULL,
	[Analyst] [varchar](50) NULL,
 CONSTRAINT [PK_T7_Sample] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_ChuKuang]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_ChuKuang](
	[ID] [varchar](50) NOT NULL,
	[WorkDate] [varchar](10) NULL,
	[WorkClassCode] [varchar](1) NULL,
	[PositionCode] [varchar](60) NULL,
	[EquipmentID] [varchar](50) NULL,
	[W1] [numeric](10, 2) NULL,
	[W2] [numeric](10, 2) NULL,
 CONSTRAINT [PK_T8_ChuKuang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_Report1]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_Report1](
	[ID] [varchar](50) NOT NULL,
	[Year] [varchar](4) NULL,
	[Month] [varchar](2) NULL,
	[Day] [varchar](2) NULL,
	[Type1_Code] [varchar](20) NULL,
	[Type1_Name] [varchar](20) NULL,
	[Y1] [int] NULL,
	[Type2_Code] [varchar](20) NULL,
	[Type2_Name] [varchar](20) NULL,
	[Val] [numeric](18, 2) NULL,
 CONSTRAINT [PK_T8_Report1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_Report1_Config]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_Report1_Config](
	[FCode] [varchar](20) NOT NULL,
	[FName] [varchar](50) NULL,
	[R1] [varchar](50) NULL,
	[R2] [varchar](50) NULL,
	[R3] [varchar](50) NULL,
 CONSTRAINT [PK_T8_Report1_Config] PRIMARY KEY CLUSTERED 
(
	[FCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_Report2]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_Report2](
	[ID] [varchar](50) NOT NULL,
	[Year] [varchar](4) NULL,
	[Month] [varchar](2) NULL,
	[Day] [varchar](2) NULL,
	[CJName] [varchar](100) NULL,
	[Y1] [int] NULL,
	[LJID] [varchar](50) NULL,
	[LJName] [varchar](100) NULL,
	[Type] [varchar](2) NULL,
	[PW_Xin] [numeric](10, 2) NULL,
	[PW_Tie] [numeric](10, 2) NULL,
	[PW_Tong] [numeric](10, 2) NULL,
	[PW_Qian] [numeric](10, 2) NULL,
	[W] [numeric](10, 2) NULL,
	[W_Xin] [numeric](10, 2) NULL,
	[W_Tie] [numeric](10, 2) NULL,
	[W_Tong] [numeric](10, 2) NULL,
	[W_Qian] [numeric](10, 2) NULL,
	[CheJian] [varchar](2) NULL,
 CONSTRAINT [PK_T8_Report2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_Report3]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_Report3](
	[ID] [varchar](50) NOT NULL,
	[Year] [varchar](4) NULL,
	[Month] [varchar](2) NULL,
	[Day] [varchar](2) NULL,
	[ZDCode] [varchar](60) NULL,
	[ZDName] [varchar](100) NULL,
	[Y1] [int] NULL,
	[CCCode] [varchar](60) NULL,
	[CCName] [varchar](100) NULL,
	[PW_Xin] [numeric](10, 2) NULL,
	[PW_Tie] [numeric](10, 2) NULL,
	[PW_Tong] [numeric](10, 2) NULL,
	[PW_Qian] [numeric](10, 2) NULL,
	[W1] [numeric](10, 2) NULL,
	[W1_Xin] [numeric](10, 2) NULL,
	[W1_Tie] [numeric](10, 2) NULL,
	[W1_Tong] [numeric](10, 2) NULL,
	[W1_Qian] [numeric](10, 2) NULL,
	[W2] [numeric](10, 2) NULL,
	[W2_Xin] [numeric](10, 2) NULL,
	[W2_Tie] [numeric](10, 2) NULL,
	[W2_Tong] [numeric](10, 2) NULL,
	[W2_Qian] [numeric](10, 2) NULL,
	[EquiName] [varchar](100) NULL,
	[CheJian] [varchar](2) NULL,
 CONSTRAINT [PK_T8_Report3] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_WR]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_WR](
	[ID] [varchar](50) NOT NULL,
	[WorkDate] [varchar](10) NULL,
	[WorkClassCode] [varchar](1) NULL,
	[WorkClassName] [varchar](50) NULL,
	[WorkManID] [varchar](50) NULL,
	[WorkManName] [varchar](50) NULL,
	[RRoleCode] [varchar](60) NULL,
	[RRoleCode_Cur] [varchar](60) NULL,
	[Status] [varchar](1) NULL,
	[Del] [varchar](1) NULL,
	[DF1] [varchar](1000) NULL,
	[DF2] [varchar](50) NULL,
	[DF3] [varchar](50) NULL,
 CONSTRAINT [PK_T8_WR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_WR_Equipment]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_WR_Equipment](
	[ID] [varchar](50) NOT NULL,
	[WRID] [varchar](50) NULL,
	[EquipmentID] [varchar](50) NULL,
 CONSTRAINT [PK_T8_WR_Equipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_WR_Equipment_D]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_WR_Equipment_D](
	[ID] [varchar](50) NOT NULL,
	[WRID] [varchar](50) NULL,
	[EquipmentID] [varchar](50) NULL,
	[FKey] [varchar](10) NULL,
	[FType] [varchar](10) NULL,
	[Fvalue0] [varchar](10) NULL,
	[FUnit0] [varchar](20) NULL,
	[FValue1] [varchar](10) NULL,
	[FUnit1] [varchar](10) NULL,
 CONSTRAINT [PK_T8_WR_Equipment_D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_WR_Position]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_WR_Position](
	[ID] [varchar](50) NOT NULL,
	[WRID] [varchar](50) NULL,
	[PositionCode] [varchar](60) NULL,
 CONSTRAINT [PK_T8_WR_Position] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_WR_Position_Data1]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_WR_Position_Data1](
	[ID] [varchar](50) NOT NULL,
	[WRID] [varchar](50) NULL,
	[PositionCode] [varchar](60) NULL,
	[FKey] [varchar](10) NULL,
	[Fvalue] [varchar](10) NULL,
	[FUnit] [varchar](10) NULL,
 CONSTRAINT [PK_T8_WR_Position_Data1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_WR_Position_Data2]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_WR_Position_Data2](
	[ID] [varchar](50) NOT NULL,
	[WRID] [varchar](50) NULL,
	[PositionCode] [varchar](60) NULL,
	[EquipmentID] [varchar](50) NULL,
 CONSTRAINT [PK_T8_WR_Position_Data2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T8_WR_Position_Data2_D]    Script Date: 2019/1/7 18:01:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T8_WR_Position_Data2_D](
	[ID] [varchar](50) NOT NULL,
	[WRID] [varchar](50) NULL,
	[PositionCode] [varchar](60) NULL,
	[EquipmentID] [varchar](50) NULL,
	[FKey] [varchar](10) NULL,
	[FType] [varchar](10) NULL,
	[Fvalue0] [varchar](10) NULL,
	[FUnit0] [varchar](20) NULL,
	[FValue1] [varchar](10) NULL,
	[FUnit1] [varchar](10) NULL,
 CONSTRAINT [PK_T8_WR_Position_Data2_D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD001001', N'OrgType', N'1', N'矿业', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD001002', N'OrgType', N'2', N'企业', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD001003', N'OrgType', N'3', N'自定义', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD002001', N'DRoleType', N'0', N'不限', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD002002', N'DRoleType', N'1', N'自身及下属', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD002003', N'DRoleType', N'2', N'自身', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD003001', N'RRoleType', N'1', N'接收', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD003002', N'RRoleType', N'2', N'审核', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD003003', N'RRoleType', N'3', N'提交', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD004001', N'JobType', N'1', N'掘进工', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD004002', N'JobType', N'2', N'安全员', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD004003', N'JobType', N'3', N'放矿工', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD004004', N'JobType', N'4', N'供矿工', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD004005', N'JobType', N'5', N'提升工', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD004006', N'JobType', N'6', N'坑内钻', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD005001', N'PositionType', N'1', N'中段', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD005002', N'PositionType', N'2', N'采场', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD005003', N'PositionType', N'3', N'作业面', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD006011', N'FieldTypeE', N'11', N'备注说明', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD006021', N'FieldTypeE', N'21', N'矿石', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD006111', N'FieldTypeM', N'11', N'备注说明', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD006112', N'FieldTypeM', N'12', N'时间', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD006121', N'FieldTypeM', N'21', N'本班进尺', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD006122', N'FieldTypeM', N'22', N'炸药量', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD006131', N'FieldTypeM', N'31', N'矿石', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD007001', N'FieldMode', N'1', N'数字', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD007002', N'FieldMode', N'2', N'文本', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD007003', N'FieldMode', N'3', N'时间', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD008000', N'FieldUnit', N'0', N'无', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD008001', N'FieldUnit', N'1', N'米', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD008010', N'FieldUnit', N'10', N'KG', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD008011', N'FieldUnit', N'11', N'吨', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD008020', N'FieldUnit', N'20', N'车', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD009001', N'EquipmentType', N'1', N'溜井', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD009002', N'EquipmentType', N'2', N'井筒', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD010001', N'WorkRecordStatus', N'1', N'草稿', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD010002', N'WorkRecordStatus', N'2', N'审核中', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD010003', N'WorkRecordStatus', N'3', N'正式', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD012001', N'ClassType', N'1', N'零点', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD012002', N'ClassType', N'2', N'八点', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD012003', N'ClassType', N'3', N'肆点', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD100001', N'ZYMStatus', N'11', N'待放炮', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD100004', N'ZYMStatus', N'22', N'通风中', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD100005', N'ZYMStatus', N'31', N'待出矿', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD100006', N'ZYMStatus', N'32', N'出矿中', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD100007', N'ZYMStatus', N'41', N'待掘进', N'0', N'0')
INSERT [dbo].[T1_DataDirc] ([ID], [Type], [DircKey], [DircTitle], [Del], [Lock]) VALUES (N'DD100008', N'ZYMStatus', N'42', N'掘进中', N'0', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101', N'1', N'101', N'系统', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101001', N'1', N'101001', N'组织结构', N'/B04_Org/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101002', N'1', N'101002', N'位置管理', N'/B05_Position/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101003', N'1', N'101003', N'员工信息', N'/B10_User/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101004', N'1', N'101004', N'页面权限', N'/B01_PRole/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101005', N'1', N'101005', N'审核权限', N'/B03_RRole/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101006', N'1', N'101006', N'数据权限', N'/B02_DRole/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'101007', N'1', N'101007', N'工种管理', N'/B06_Job/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'103', N'1', N'103', N'导入', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'103001', N'1', N'103001', N'员工导入', N'/B10_User/PageList_Excel', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'103002', N'1', N'103002', N'计划导入', N'/D01_Plan/PlanList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'103003', N'1', N'103003', N'验收导入', N'/D02_Check/CheckList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'105', N'1', N'105', N'设备管理', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'105001', N'1', N'105001', N'设备类型', N'/C02_EquipmentType/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'105002', N'1', N'105002', N'设备管理', N'/C01_Equipment/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'105003', N'1', N'105003', N'数据录入', N'/C03_EquiDataEntry/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'105004', N'1', N'105004', N'设备数据', N'/C04_EquiData/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'106', N'1', N'106', N'井下数据管理', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'106001', N'1', N'106001', N'数据录入', N'/C11_MineDataEntry/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'106002', N'1', N'106002', N'井下数据', N'/C12_MineData/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'107', N'1', N'107', N'审核流程', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'107001', N'1', N'107001', N'数据审核', N'/C21_RManage/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'109', N'1', N'109', N'报表', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'109001', N'1', N'109001', N'汇总报表', N'/F01_Report/List', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'109002', N'1', N'109002', N'车间日报', N'/F02_Report/List', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'109003', N'1', N'109003', N'采场日报', N'/F03_Report/List', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'111', N'1', N'111', N'采样', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'111001', N'1', N'111001', N'采样记录', N'/E01_Sample/Index', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'111002', N'1', N'111002', N'采样录入', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'113', N'1', N'113', N'公告管理', NULL, N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'113001', N'1', N'113001', N'公告列表', N'/C91_MessageBoard/PageList', N'0')
INSERT [dbo].[T1_Page] ([Code], [Type], [OrderBy], [Title], [Url], [Del]) VALUES (N'201', N'2', N'201', N'手持机', NULL, N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000001', N'吴珏', N'wuj', N'zRWBd27+hNU=', N'001', N'PR0000000001', N'001', N'0', NULL, N'1024001', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000002', N'钟继伟', N'zhongjw', N'zRWBd27+hNU=', N'001001', N'PR0000000001', N'001001001', N'0', N'2', N'1024002', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000003', N'汪天正', N'wangtz', N'zRWBd27+hNU=', N'001001001', N'PR0000000002', N'001001001', N'1', N'3', N'1024003', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000004', N'钱文龙', N'qianwl', N'zRWBd27+hNU=', N'001001001001', N'PR0000000002', N'001001', N'2', N'4', N'1024004', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000005', N'包扬', N'baoy', N'zRWBd27+hNU=', N'001001002', N'PR0000000002', N'001001001', N'2', N'5', N'1024005', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000006', N'张林', N'zhangl', N'zRWBd27+hNU=', N'001001001001', N'PR0000000001', N'001001', N'1', N'5', N'1024006', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000007', N'吴彬彬', N'wubb', N'zRWBd27+hNU=', N'001001002001', N'PR0000000002', N'001001001', N'2', N'6', N'1024007', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000008', N'张佳兴', N'zhangjx', N'zRWBd27+hNU=', N'001001002001', N'PR0000000002', N'001001001', N'1', N'5', N'1024008', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000009', N'秦镭', N'qinl', N'zRWBd27+hNU=', N'001001002001', N'PR0000000002', N'001001001', N'2', N'1', N'1024009', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000010', N'高志', N'gaoz', N'zRWBd27+hNU=', N'001001001001', N'PR0000000002', N'001001001', N'2', N'3', N'1024010', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000011', N'温州建设放矿工A', N'1111', N'Z7yZ5TwmrYQ=', N'001001001001', N'PR0000000005', N'001002001001', N'2', N'3', N'1111', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000012', N'温州建设放矿工B', N'1112', N'Z7yZ5TwmrYQ=', N'001001001001', N'PR0000000005', N'001002001001', N'2', N'3', N'1112', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000013', N'温州建设放矿工管理A', N'111', N'Z7yZ5TwmrYQ=', N'001001001001', N'PR0000000004', N'001002001', N'1', N'3', N'111', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000014', N'温州建设放矿工管理B', N'112', N'Z7yZ5TwmrYQ=', N'001001001001', N'PR0000000004', N'001002001', N'1', N'3', N'112', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000015', N'车间管理A', N'11', N'Z7yZ5TwmrYQ=', N'001001', N'PR0000000003', N'001002', N'1', N'3', N'11', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000016', N'运营层', N'1', N'Z7yZ5TwmrYQ=', N'001', N'PR0000000002', N'001', N'0', N'3', N'1', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000017', N'温州建设供矿工A', N'2111', N'Z7yZ5TwmrYQ=', N'001001001001', N'PR0000000005', N'001002003001', N'2', N'4', N'2111', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000018', N'温州建设安全员A', N'3111', N'Z7yZ5TwmrYQ=', N'001001001001', N'PR0000000005', N'001002005001', N'2', N'2', N'3111', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000019', N'温州建设掘进工A', N'4111', N'Z7yZ5TwmrYQ=', N'001001001001', N'PR0000000005', N'001002004001', N'2', N'1', N'4111', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000020', N'11111', N'11111', N'zRWBd27+hNU=', N'001001002001', N'PR0000000001', N'001002001001', N'0', NULL, NULL, N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000021', N'11111', N'11111', N'zRWBd27+hNU=', N'001001002001', N'', N'001001001', N'2', N'', N'', N'0')
INSERT [dbo].[T1_User] ([ID], [Name], [LoginName], [Password], [OrgCode], [PRoleID], [RRoleCode], [DRoleType], [JobCode], [UserKey], [Del]) VALUES (N'UR0000000022', N'1100111', N'1100111', N'zRWBd27+hNU=', N'001001002001', N'', N'001001001', N'2', N'', N'', N'0')
INSERT [dbo].[T1_User_Admin] ([ID], [Name], [LoginName], [Password]) VALUES (N'TUA001', N'超级管理员', N'Admin', N'Z7yZ5TwmrYQ=')
INSERT [dbo].[T2_DRole] ([ID], [Title], [Type], [Del], [Lock]) VALUES (N'DR0000000001', N'不限', N'0', N'0', N'0')
INSERT [dbo].[T2_DRole] ([ID], [Title], [Type], [Del], [Lock]) VALUES (N'DR0000000002', N'自身以及下属', N'1', N'0', N'0')
INSERT [dbo].[T2_DRole] ([ID], [Title], [Type], [Del], [Lock]) VALUES (N'DR0000000003', N'自身', N'2', N'0', N'0')
INSERT [dbo].[T2_Org] ([ID], [Code], [Type], [Title], [STitle], [Remark], [Del], [Lock]) VALUES (N'ORG0000000001', N'001', N'1', N'红岭矿业', N'红岭矿业', N'红岭矿业', N'0', N'1')
INSERT [dbo].[T2_Org] ([ID], [Code], [Type], [Title], [STitle], [Remark], [Del], [Lock]) VALUES (N'ORG0000000002', N'001001', N'1', N'采掘车间', N'采掘车间', N'红岭矿业 - 采掘车间', N'0', N'0')
INSERT [dbo].[T2_Org] ([ID], [Code], [Type], [Title], [STitle], [Remark], [Del], [Lock]) VALUES (N'ORG0000000003', N'001001001', N'1', N'采掘一车间', N'采掘一车间', N'红岭矿业 - 采掘车间 - 采掘一车间', N'0', N'0')
INSERT [dbo].[T2_Org] ([ID], [Code], [Type], [Title], [STitle], [Remark], [Del], [Lock]) VALUES (N'ORG0000000004', N'001001002', N'1', N'采掘二车间', N'采掘二车间', N'红岭矿业 - 采掘车间 - 采掘二车间', N'0', N'0')
INSERT [dbo].[T2_Org] ([ID], [Code], [Type], [Title], [STitle], [Remark], [Del], [Lock]) VALUES (N'ORG0000000005', N'001001001001', N'2', N'温州建设', N'温州建设', N'红岭矿业 - 采掘车间 - 采掘一车间 - 温州建设', N'0', N'0')
INSERT [dbo].[T2_Org] ([ID], [Code], [Type], [Title], [STitle], [Remark], [Del], [Lock]) VALUES (N'ORG0000000006', N'001001002001', N'2', N'温州二井', N'温州二井', N'红岭矿业 - 采掘车间 - 采掘二车间 - 温州二井', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000001', N'001', N'1', N'955m', N'955M', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000002', N'002', N'1', N'905m', N'905M', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000003', N'003', N'1', N'855m', N'855M', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000004', N'004', N'1', N'805m', N'805M', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000005', N'005', N'1', N'755m', N'755M', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000006', N'006', N'1', N'705m', N'705M', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000007', N'001001', N'2', N'3212采场', N'955M - 3212采场', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000008', N'001002', N'2', N'3110采场', N'955M - 3110采场', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000009', N'004001', N'2', N'6107矿房', N'805M - 6107矿房', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000010', N'001001001', N'3', N'32121', N'955M - 3212采场 - 32121', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000011', N'001001002', N'3', N'32122', N'955M - 3212采场 - 32122', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000012', N'001002001', N'3', N'31101', N'955M - 3110采场 - 31101', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000013', N'004001001', N'3', N'61071', N'805M - 6107矿房 - 61071', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000014', N'005001', N'2', N'7113矿柱', N'755M - 7113矿柱', N'0', N'0')
INSERT [dbo].[T2_Position] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'TP0000000015', N'005001001', N'3', N'71131', N'755M - 7113矿柱 - 71131', N'0', N'0')
INSERT [dbo].[T2_Position_Org] ([PositionCode], [OrgCode]) VALUES (N'001', N'001001001001')
INSERT [dbo].[T2_Position_Org] ([PositionCode], [OrgCode]) VALUES (N'002', N'001001001001')
INSERT [dbo].[T2_Position_Org] ([PositionCode], [OrgCode]) VALUES (N'003', N'001001001001')
INSERT [dbo].[T2_Position_Org] ([PositionCode], [OrgCode]) VALUES (N'004', N'001001001001')
INSERT [dbo].[T2_Position_Org] ([PositionCode], [OrgCode]) VALUES (N'005', N'001001002001')
INSERT [dbo].[T2_Position_Org] ([PositionCode], [OrgCode]) VALUES (N'006', N'001001002001')
INSERT [dbo].[T2_PRole] ([ID], [Title], [Remark], [Del], [Lock]) VALUES (N'PR0000000001', N'管理员', N'所有页面', N'0', N'0')
INSERT [dbo].[T2_PRole] ([ID], [Title], [Remark], [Del], [Lock]) VALUES (N'PR0000000002', N'矿业运营层', N'矿业运营层', N'0', N'0')
INSERT [dbo].[T2_PRole] ([ID], [Title], [Remark], [Del], [Lock]) VALUES (N'PR0000000003', N'矿业管理层', N'矿业管理层', N'0', N'0')
INSERT [dbo].[T2_PRole] ([ID], [Title], [Remark], [Del], [Lock]) VALUES (N'PR0000000004', N'企业管理层', N'企业管理层', N'0', N'0')
INSERT [dbo].[T2_PRole] ([ID], [Title], [Remark], [Del], [Lock]) VALUES (N'PR0000000005', N'企业员工层', N'企业员工层', N'0', N'0')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101003')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101004')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101005')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101006')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'101007')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'103')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'103001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'103002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'103003')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'105')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'105001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'105002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'107')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'107001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'3', N'107002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101003')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101004')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101005')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101006')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'101007')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'103')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'103001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'103002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'103003')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'105')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'105001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'105002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'105003')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'105004')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'106')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'106001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'106002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'107')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'107001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'109')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'111')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'111001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'111002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'113')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000001', N'113001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'103')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'103001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'103002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'103003')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'105')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'105002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'105004')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'106')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'106002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'109')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'111')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'111001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'113')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000002', N'113001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'105')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'105002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'105004')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'106')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'106002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'107')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'107001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'109')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'111')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'111001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'113')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000003', N'113001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'105')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'105002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'105004')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'106')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'106002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'107')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'107001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'109')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'111')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'111001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'113')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000004', N'113001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000005', N'105')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000005', N'105003')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000005', N'105004')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000005', N'106')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000005', N'106001')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR0000000005', N'106002')
INSERT [dbo].[T2_PRole_Detail] ([PRoleID], [PageCode]) VALUES (N'PR5', N'')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000001', N'001', N'1', N'矿业运营层', NULL, N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000002', N'001001', N'2', N'企业管理层', N'', N'1', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000003', N'001001001', N'3', N'企业员工', N'', N'1', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000004', N'001002', N'2', N'矿业车间管理层', N'矿业车间管理层', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000005', N'001002001', N'2', N'温州建设放矿工管理', N'温州建设放矿工管理', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000006', N'001002001001', N'3', N'温州建设放矿工', N'温州建设放矿工', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000007', N'001002002', N'2', N'温州二井放矿工管理', N'温州二井放矿工管理', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000008', N'001002002001', N'3', N'温州二井放矿工', N'温州二井放矿工', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000009', N'001002003', N'2', N'温州建设供矿工管理', N'温州建设供矿工管理', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000010', N'001002003001', N'3', N'温州建设供矿工', N'温州建设供矿工', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000011', N'001002004', N'2', N'温州建设掘进工管理', N'温州建设掘进工管理', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000012', N'001002004001', N'3', N'温州建设掘进工', N'温州建设掘进工', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000013', N'001002005', N'2', N'温州建设安全员管理', N'温州建设安全员管理', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000014', N'001002006', N'2', N'温州建设技术员管理', N'温州建设技术员管理', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000015', N'001002005001', N'3', N'温州建设安全员', N'温州建设安全员', N'0', N'0')
INSERT [dbo].[T2_RRole] ([ID], [Code], [Type], [Title], [Remark], [Del], [Lock]) VALUES (N'RR0000000016', N'001002006001', N'3', N'温州建设技术员', N'温州建设技术员', N'0', N'0')
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201810290100001', N'WR00000000112018102901', N'001002001001', N'UR0000000011', N'0', N'1111', CAST(N'2018-11-02T14:29:18.020' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201810290100002', N'WR00000000112018102901', N'001002001', N'UR0000000013', N'1', N'2', CAST(N'2018-11-02T14:51:22.867' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201810290100003', N'WR00000000112018102901', N'001002001001', N'UR0000000011', N'0', N'3', CAST(N'2018-11-02T15:36:57.560' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201810290100004', N'WR00000000112018102901', N'001002001', N'UR0000000013', N'0', N'4', CAST(N'2018-11-02T15:37:28.513' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201810290100005', N'WR00000000112018102901', N'001002', N'UR0000000015', N'0', N'5', CAST(N'2018-11-02T15:37:58.763' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201811060200001', N'WR00000000112018110602', N'001002001001', N'TUA001', N'0', N'', CAST(N'2018-11-07T15:47:30.280' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201811060200002', N'WR00000000112018110602', N'001002001', N'TUA001', N'1', N'2', CAST(N'2018-11-07T16:02:18.937' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201811060200003', N'WR00000000112018110602', N'001002001001', N'TUA001', N'0', N'3', CAST(N'2018-11-07T16:02:41.700' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201811060200004', N'WR00000000112018110602', N'001002001', N'TUA001', N'0', N'4', CAST(N'2018-11-07T16:02:52.623' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201811060200005', N'WR00000000112018110602', N'001002', N'TUA001', N'0', N'5', CAST(N'2018-11-07T16:03:03.327' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201811250100001', N'WR00000000112018112501', N'001002001001', N'UR0000000011', N'0', N'', CAST(N'2018-11-30T15:51:36.097' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201811290200001', N'WR00000000112018112902', N'001002001001', N'UR0000000011', N'0', N'', CAST(N'2018-12-20T15:58:50.517' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201812180100001', N'WR00000000112018121801', N'001002001001', N'UR0000000011', N'0', N'', CAST(N'2018-12-18T16:15:27.947' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201812200100001', N'WR00000000112018122001', N'001002001001', N'UR0000000011', N'0', N'', CAST(N'2018-12-20T15:17:44.137' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000011201812200100002', N'WR00000000112018122001', N'001002001', N'UR0000000011', N'0', N'', CAST(N'2018-12-20T18:34:55.507' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000012201812200200001', N'WR00000000122018122002', N'001002001001', N'UR0000000012', N'0', N'', CAST(N'2018-12-20T15:59:44.547' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201811290200001', N'WR00000000172018112902', N'001002003001', N'UR0000000017', N'0', N'', CAST(N'2018-12-20T16:00:25.203' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201812170200001', N'WR00000000172018121702', N'001002003001', N'UR0000000017', N'0', N'', CAST(N'2018-12-21T09:43:29.690' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201812180200001', N'WR00000000172018121802', N'001002003001', N'UR0000000017', N'0', N'', CAST(N'2018-12-20T19:23:15.043' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201812190200001', N'WR00000000172018121902', N'001002003001', N'UR0000000017', N'0', N'', CAST(N'2018-12-21T09:46:11.330' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201812200200001', N'WR00000000172018122002', N'001002003001', N'UR0000000017', N'0', N'', CAST(N'2018-12-20T19:23:05.887' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201812210200001', N'WR00000000172018122102', N'001002003001', N'UR0000000017', N'0', N'', CAST(N'2018-12-21T08:46:19.073' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201812210200002', N'WR00000000172018122102', N'001002003', N'UR0000000017', N'0', N'', CAST(N'2018-12-21T09:12:22.577' AS DateTime))
INSERT [dbo].[T2_RRole_OperLog] ([ID], [WorkRecordID], [RRoleCode], [UserID], [Result], [Remark], [RDate]) VALUES (N'RRO0000000017201812210200003', N'WR00000000172018122102', N'001002', N'UR0000000017', N'0', N'', CAST(N'2018-12-21T09:44:22.723' AS DateTime))
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000001')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000002')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000003')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000004')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000005')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000006')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000007')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000008')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000001', N'PR0000000011')
INSERT [dbo].[T2_RRole_User] ([RRoleID], [UserID]) VALUES (N'RR0000000006', N'UR0000000004')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000001001', N'Job', N'1', 1, N'Job_1_1', N'本班进尺', N'1', N'21', N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000001002', N'Job', N'1', 2, N'Job_1_2', N'炸药量', N'10', N'22', N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000001003', N'Job', N'1', 3, N'Job_1_3', N'掘进时间', N'0', N'12', N'3')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000001004', N'Job', N'1', 4, N'Job_1_4', N'备注说明', N'0', N'11', N'2')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000002001', N'Job', N'2', 1, N'Job_2_1', N'爆破时间', N'0', N'12', N'3')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000002002', N'Job', N'2', 2, N'Job_2_2', N'通风时间', N'0', N'12', N'3')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000003101', N'Job', N'3', 1, N'Job_3_1', N'放矿', N'11', N'23', N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000003191', N'Job', N'3', 2, N'Job_3_2', N'放矿时间', N'0', N'12', N'3')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000003301', N'Job', N'3', 5, N'Job_3_5', N'备注说明', N'0', N'11', N'2')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000004101', N'Job', N'4', 1, N'Job_4_1', N'出渣', N'11', N'23', N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000004191', N'Job', N'4', 3, N'Job_4_2', N'出渣时间', N'0', N'12', N'3')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000004301', N'Job', N'4', 7, N'Job_4_5', N'备注说明', N'0', N'11', N'2')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000010001', N'Equipment', N'1', 1, N'Equipment_1_1', N'提升量', N'11', N'21', N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000010003', N'Equipment', N'1', 3, N'Equipment_1_3', N'备注说明', N'0', N'11', N'2')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000020001', N'Equipment', N'2', 1, N'Equipment_2_1', N'提升量', N'11', N'21', N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000020003', N'Equipment', N'2', 3, N'Equipment_2_3', N'备注说明', N'0', N'11', N'2')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000101001', N'Foreman', N'1', 1, N'F_1_1', N'本班进尺', N'1', NULL, N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000101002', N'Foreman', N'1', 2, N'F_1_2', N'炸药量', N'10', NULL, N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000102001', N'Foreman', N'2', 1, N'F_2_1', N'供矿', N'11', NULL, N'1')
INSERT [dbo].[T3_Dynamic_Field] ([ID], [Type1], [Type2], [I], [FieldKey], [FieldName], [FieldUnit], [FieldType], [FieldMode]) VALUES (N'DF0000103001', N'Foreman', N'3', 1, N'F_3_1', N'放矿', N'11', NULL, N'1')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Equipment_1_1', N'1', N'矿石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Equipment_1_1', N'2', N'废石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Equipment_2_1', N'1', N'矿石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Equipment_2_1', N'2', N'废石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'F_2_1', N'1', N'矿石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'F_2_1', N'2', N'废石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'F_3_1', N'1', N'矿石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'F_3_1', N'2', N'废石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Job_3_1', N'1', N'矿石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Job_3_1', N'2', N'废石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Job_4_1', N'1', N'矿石')
INSERT [dbo].[T3_Dynamic_FieldType] ([DFKey], [Type], [Title]) VALUES (N'Job_4_1', N'2', N'废石')
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'F_2_1', N'1', N'1立方铲运机', CAST(1.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'F_2_1', N'2', N'2立方铲运机', CAST(3.0000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'F_2_1', N'3', N'3立方铲运机', CAST(4.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'F_2_1', N'4', N'坑内卡车', CAST(4.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'F_3_1', N'1', N'2立方米矿车', CAST(4.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'F_3_1', N'2', N'0.75立方米矿车', CAST(1.6780 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'Job_3_1', N'1', N'2立方米矿车', CAST(4.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'Job_3_1', N'2', N'0.75立方米矿车', CAST(1.6780 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'Job_4_1', N'1', N'1立方铲运机', CAST(1.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'Job_4_1', N'2', N'2立方铲运机', CAST(3.0000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'Job_4_1', N'3', N'3立方铲运机', CAST(4.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Dynamic_FieldUnit] ([DFKey], [Type], [Title], [Unit_0_Rate]) VALUES (N'Job_4_1', N'4', N'坑内卡车', CAST(4.5000 AS Numeric(20, 4)))
INSERT [dbo].[T3_Equipment] ([ID], [Title], [Type], [Remark], [Del], [Lock]) VALUES (N'TE0000000001', N'1#溜井', N'1', N'主溜井', N'0', N'0')
INSERT [dbo].[T3_Equipment] ([ID], [Title], [Type], [Remark], [Del], [Lock]) VALUES (N'TE0000000002', N'2#溜井', N'1', N'6102溜井', N'0', N'0')
INSERT [dbo].[T3_Equipment] ([ID], [Title], [Type], [Remark], [Del], [Lock]) VALUES (N'TE0000000003', N'3#溜井', N'1', N'6103溜井', N'0', N'0')
INSERT [dbo].[T3_Equipment] ([ID], [Title], [Type], [Remark], [Del], [Lock]) VALUES (N'TE0000000004', N'4#溜井', N'1', N'6104溜井', N'0', N'0')
INSERT [dbo].[T3_Equipment] ([ID], [Title], [Type], [Remark], [Del], [Lock]) VALUES (N'TE0000000005', N'5#溜井', N'1', N'7101溜井', N'0', N'0')
INSERT [dbo].[T3_Equipment] ([ID], [Title], [Type], [Remark], [Del], [Lock]) VALUES (N'TE0000000006', N'6#废石溜井', N'1', N'6#废石溜井', N'0', N'0')
INSERT [dbo].[T3_Equipment] ([ID], [Title], [Type], [Remark], [Del], [Lock]) VALUES (N'TE0000000007', N'7#溜井', N'1', N'7201溜井', N'0', N'0')
INSERT [dbo].[T3_Equipment_Position] ([EquipmentID], [PositionCode]) VALUES (N'TE0000000001', N'001001')
INSERT [dbo].[T3_Equipment_Position] ([EquipmentID], [PositionCode]) VALUES (N'TE0000000001', N'001002')
INSERT [dbo].[T3_Equipment_Position] ([EquipmentID], [PositionCode]) VALUES (N'TE0000000001', N'004001')
INSERT [dbo].[T3_Equipment_Position] ([EquipmentID], [PositionCode]) VALUES (N'TE0000000002', N'004001')
INSERT [dbo].[T3_Equipment_Position] ([EquipmentID], [PositionCode]) VALUES (N'TE0000000003', N'004001')
INSERT [dbo].[T3_Equipment_Position] ([EquipmentID], [PositionCode]) VALUES (N'TE0000000005', N'005001')
INSERT [dbo].[T3_EquipmentType] ([ID], [Title], [Type], [Del], [Lock]) VALUES (N'ET0000000001', N'溜井', N'1', N'0', N'0')
INSERT [dbo].[T3_EquipmentType] ([ID], [Title], [Type], [Del], [Lock]) VALUES (N'ET0000000002', N'井筒', N'2', N'0', N'0')
INSERT [dbo].[T3_Job] ([ID], [Code], [Title], [Del]) VALUES (N'TJ0000000001', N'1', N'掘进工', N'0')
INSERT [dbo].[T3_Job] ([ID], [Code], [Title], [Del]) VALUES (N'TJ0000000002', N'2', N'安全员', N'0')
INSERT [dbo].[T3_Job] ([ID], [Code], [Title], [Del]) VALUES (N'TJ0000000003', N'3', N'放矿工', N'0')
INSERT [dbo].[T3_Job] ([ID], [Code], [Title], [Del]) VALUES (N'TJ0000000004', N'4', N'出渣工', N'0')
INSERT [dbo].[T3_Job] ([ID], [Code], [Title], [Del]) VALUES (N'TJ0000000005', N'5', N'领导', N'0')
INSERT [dbo].[T3_Job] ([ID], [Code], [Title], [Del]) VALUES (N'TJ0000000006', N'6', N'技术人员', N'0')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_111', N'主溜井', N't', N'w', N'num', N'Job_3_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_112', N'主溜井', N't', N'w', N'num', N'Job_3_3')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_121', N'6102溜井', N't', N'w', N'num', N'Job_3_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_122', N'6102溜井', N't', N'w', N'num', N'Job_3_3')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_131', N'6103溜井', N't', N'w', N'num', N'Job_3_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_132', N'6103溜井', N't', N'w', N'num', N'Job_3_3')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_141', N'6104溜井', N't', N'w', N'num', N'Job_3_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_142', N'6104溜井', N't', N'w', N'num', N'Job_3_3')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_151', N'7101溜井', N't', N'w', N'num', N'Job_3_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_152', N'7101溜井', N't', N'w', N'num', N'Job_3_3')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_161', N'7201溜井', N't', N'w', N'num', N'Job_3_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_162', N'7201溜井', N't', N'w', N'num', N'Job_3_3')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_211', N'箕斗井', N't', N'w', N'num', N'Job_5_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_212', N'箕斗井', N't', N'w', N'num', N'Job_5_2')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_221', N'进风井', N't', N'w', N'num', N'Job_5_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_222', N'进风井', N't', N'w', N'num', N'Job_5_2')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_231', N'原进风井', N't', N'w', N'num', N'Job_5_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_232', N'原进风井', N't', N'w', N'num', N'Job_5_2')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_241', N'主井', N't', N'w', N'num', N'Job_5_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_242', N'主井', N't', N'w', N'num', N'Job_5_2')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_251', N'副井', N't', N'w', N'num', N'Job_5_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B1_252', N'副井', N't', N'w', N'num', N'Job_5_2')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_01', N'工程类型', N'性质', N'', N'str', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_02', N'台效', N'm/班', N'c', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_03', N'台班', N'个', N'c', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_04', N'断面积', N'm2', N'', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_05', N'长度', N'm', N'w', N'num', N'Job_1_1')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_06', N'体积', N'm3', N'c', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_07', N'掘进量', N't', N'c', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_08', N'折合标米', N'm', N'c', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_09', N'副产', N't', N'', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_10', N'施工时间', N'', N'', N'str', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B4_11', N'机台', N'', N'c', N'str', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B7_09', N'出矿量', N't', N'c', N'num', N'')
INSERT [dbo].[T4_Config] ([Code], [Remark1], [Unit1], [Type1], [Type2], [DFKey]) VALUES (N'B7_10', N'日供矿量', N't', N'w', N'num', N'Job_4_1')
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001001', 31, N'32', CAST(N'2018-12-20T18:34:55.507' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001005', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001006', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001007', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004001008', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004005001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004006001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004007001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004008001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'004009001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00400A001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00400B001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00400C001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005001001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005001002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005001003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005001004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005002001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005002002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005005', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005006', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005007', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005008', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'005009', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00500A', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00500B001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00500C001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00500D001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00500E001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00500F001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00500G001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006001001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006001002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006001003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006005', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006006', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006007', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006008001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'006009001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00600A001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007001001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007001002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007001003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007005', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007006', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007007', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007008001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'007009001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00700A001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008001001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008001002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008001003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008002001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008002002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008005', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008006001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008007001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008008001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'008009001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00800A001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009001001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009001002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009001003', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009002001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009003001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009004', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009004001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009004002', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009005001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009006', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009007001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009008001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'009009001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00900A001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00900B001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00900C001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00900D001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP] ([Month], [PositionCode], [WorkDayCount], [Status], [StatusChangeDate]) VALUES (N'2018-12', N'00900E001', 31, N'11', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_111', N'34931.248')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_121', N'28300.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_131', N'20100.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_141', N'10900.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_151', N'17355.772')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_161', N'900.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_211', N'126244.860')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_221', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_222', N'15127.624')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_231', N'3800.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_232', N'1421.550')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_242', N'10743.264')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'', N'B1_252', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001001', N'B4_05', N'4.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001002', N'B4_05', N'25.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001003', N'B4_05', N'50.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001004', N'B4_05', N'50.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001005', N'B4_05', N'40.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001006', N'B4_05', N'30.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001007', N'B4_05', N'25.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004001008', N'B4_05', N'16.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004002', N'B7_09', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004003', N'B7_09', N'5000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004004', N'B7_09', N'200.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004005001', N'B4_05', N'4.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004006001', N'B4_05', N'25.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004007001', N'B4_05', N'50.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004008001', N'B4_05', N'50.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'004009001', N'B4_05', N'40.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00400A001', N'B4_05', N'30.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00400B001', N'B4_05', N'25.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00400C001', N'B4_05', N'16.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005001001', N'B4_05', N'20.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005001002', N'B4_05', N'25.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005001003', N'B4_05', N'4.300')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005001004', N'B4_05', N'15.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005002001', N'B4_05', N'20.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005002002', N'B4_05', N'7.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005003', N'B7_09', N'6000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005004', N'B7_09', N'800.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005005', N'B7_09', N'3000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005006', N'B7_09', N'1000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005007', N'B7_09', N'3000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005008', N'B7_09', N'600.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'005009', N'B7_09', N'800.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00500A', N'B7_09', N'6500.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00500B001', N'B4_05', N'20.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00500C001', N'B4_05', N'25.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00500D001', N'B4_05', N'4.300')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00500E001', N'B4_05', N'15.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00500F001', N'B4_05', N'20.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00500G001', N'B4_05', N'7.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006001001', N'B4_05', N'42.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006001002', N'B4_05', N'10.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006001003', N'B4_05', N'14.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006002', N'B7_09', N'28000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006003', N'B7_09', N'200.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006004', N'B7_09', N'9000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006005', N'B7_09', N'21800.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006006', N'B7_09', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006007', N'B7_09', N'300.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006008001', N'B4_05', N'42.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'006009001', N'B4_05', N'10.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00600A001', N'B4_05', N'14.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007001001', N'B4_05', N'100.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007001002', N'B4_05', N'50.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007001003', N'B4_05', N'60.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007002', N'B7_09', N'3000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007003', N'B7_09', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007004', N'B7_09', N'3000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007005', N'B7_09', N'10600.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007006', N'B7_09', N'10600.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007007', N'B7_09', N'500.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007008001', N'B4_05', N'100.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'007009001', N'B4_05', N'50.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00700A001', N'B4_05', N'60.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008001001', N'B4_05', N'100.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008001002', N'B4_05', N'60.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008001003', N'B4_05', N'20.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008002001', N'B4_05', N'10.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008002002', N'B4_05', N'9.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008003', N'B7_09', N'1500.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008004', N'B7_09', N'3000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008005', N'B7_09', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008006001', N'B4_05', N'100.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008007001', N'B4_05', N'60.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008008001', N'B4_05', N'10.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'008009001', N'B4_05', N'9.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00800A001', N'B4_05', N'20.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009001', N'B7_09', N'1000.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009001001', N'B4_05', N'10.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009001002', N'B4_05', N'3.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009001003', N'B4_05', N'3.500')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009002001', N'B4_05', N'5.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009003001', N'B4_05', N'30.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009004', N'B7_09', N'800.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009004001', N'B4_05', N'5.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009004002', N'B4_05', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009005001', N'B4_05', N'60.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009006', N'B7_09', N'2000.000')
GO
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009007001', N'B4_05', N'10.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009008001', N'B4_05', N'3.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'009009001', N'B4_05', N'3.500')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00900A001', N'B4_05', N'5.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00900B001', N'B4_05', N'30.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00900C001', N'B4_05', N'5.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00900D001', N'B4_05', N'0.000')
INSERT [dbo].[T4_MP_Detail_1] ([Month], [PositionCode], [ConfigCode], [Val]) VALUES (N'2018-12', N'00900E001', N'B4_05', N'60.000')
INSERT [dbo].[T5_MessageBoard] ([ID], [PositionCode], [UserID], [Remark], [Date]) VALUES (N'MB0000000001', N'004001008', N'TUA001', N'大家好123123', CAST(N'2018-12-04T14:54:31.380' AS DateTime))
INSERT [dbo].[T5_MessageBoard] ([ID], [PositionCode], [UserID], [Remark], [Date]) VALUES (N'TM0000000002', N'004001001', N'TUA001', N'地面打扫干净', CAST(N'2018-12-04T11:17:08.367' AS DateTime))
INSERT [dbo].[T5_WorkRecord] ([ID], [RecordType], [WorkDate], [WorkClassCode], [WorkClassName], [WorkHour], [WorkManID], [WorkManName], [RRoleCode], [RRoleCode_Cur], [Status], [Del], [DF1], [DF2], [DF3]) VALUES (N'WR00000000112018122101', N'E', N'2018-12-21', N'1', N'零点', CAST(2.0 AS Numeric(3, 1)), N'UR0000000011', N'温州建设放矿工A', N'001002001001', N'001002001', N'2', N'0', N'提升量: 20.000 吨', NULL, NULL)
INSERT [dbo].[T5_WorkRecord_Detail] ([ID], [WorkRecordID], [EquipmentID], [PositionCode], [WorkHour], [WhereAbout], [DF1], [DF2], [DF3]) VALUES (N'WR00000000112018122101001', N'WR00000000112018122101', N'TE0000000001', NULL, CAST(1.0 AS Numeric(3, 1)), NULL, N'提升量: 10.000 吨', NULL, NULL)
INSERT [dbo].[T5_WorkRecord_Detail_Field] ([ID], [WorkRecordDetailID], [FieldKey], [FieldType], [FieldValue], [FieldUnit], [FieldValue0], [FieldUnit0]) VALUES (N'WR00000000112018122101001001', N'WR00000000112018122101001', N'Equipment_1_1', N'1', N'10', N'吨', N'10', N'吨')
INSERT [dbo].[T5_WorkRecord_Detail_Field] ([ID], [WorkRecordDetailID], [FieldKey], [FieldType], [FieldValue], [FieldUnit], [FieldValue0], [FieldUnit0]) VALUES (N'WR00000000112018122101001002', N'WR00000000112018122101001', N'Equipment_1_1', N'2', N'10', N'吨', N'10', N'吨')
INSERT [dbo].[T6_Check] ([ID], [YM], [FileName], [FileNameS], [UploadTime]) VALUES (N'CH0000000003', N'2018/04', N'2018年4月份生产验收最终.xlsx', N'149bd714-3c46-489a-b719-d432c0b603de.xlsx', CAST(N'2018-12-26T09:54:35.000' AS DateTime))
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000001', N'CH0000000003', N'日历天数', N'', N'天', N'31')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000002', N'CH0000000003', N'3000吨/日选厂', N'生产', N'天', N'25')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000003', N'CH0000000003', N'3000吨/日选厂', N'检修', N'天', N'6')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000004', N'CH0000000003', N'2000吨/日选厂', N'生产', N'天', N'29')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000005', N'CH0000000003', N'2000吨/日选厂', N'检修', N'天', N'2')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000006', N'CH0000000003', N'箕斗井', N'生产', N'天', N'25')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000007', N'CH0000000003', N'箕斗井', N'检修', N'天', N'6')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000008', N'CH0000000003', N'采掘总量', N'', N'吨', N'108980')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000009', N'CH0000000003', N'采掘总量', N'采掘车间', N'吨', N'6000')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000010', N'CH0000000003', N'采掘总量', N'温州建设', N'吨', N'46346')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000011', N'CH0000000003', N'采掘总量', N'温州二井', N'吨', N'47990')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000012', N'CH0000000003', N'采掘总量', N'项目工程', N'吨', N'8644')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000013', N'CH0000000003', N'采矿量', N'', N'吨', N'83300')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000014', N'CH0000000003', N'掘进量', N'', N'吨', N'25680')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000015', N'CH0000000003', N'掘进米', N'', N'自然米', N'1253')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000016', N'CH0000000003', N'掘进米', N'', N'标准米', N'2211')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000017', N'CH0000000003', N'充填量', N'', N'立方米', N'9608')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000018', N'CH0000000003', N'选矿处理量', N'', N'吨', N'130450')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000019', N'CH0000000003', N'锌入选品位', N'', N'%', N'1.69')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000020', N'CH0000000003', N'铁入选品位', N'', N'%', N'24.08')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000021', N'CH0000000003', N'铜入选品位', N'', N'%', N'0.13')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000022', N'CH0000000003', N'锌综合回收率', N'', N'%', N'92.00')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000023', N'CH0000000003', N'铁综合回收率', N'', N'%', N'45.00')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000024', N'CH0000000003', N'铜综合回收率', N'', N'%', N'65.00')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000025', N'CH0000000003', N'锌金属产量', N'', N'吨', N'2100')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000026', N'CH0000000003', N'铁精矿产量', N'', N'吨', N'21422')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000027', N'CH0000000003', N'铜金属产量', N'', N'吨', N'113')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000028', N'CH0000000003', N'井下供矿', N'', N'吨', N'130045')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000029', N'CH0000000003', N'各井筒矿石提升量', N'箕斗井', N'吨', N'126244.86')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000030', N'CH0000000003', N'各井筒矿石提升量', N'日均提升', N'吨', N'5049.7944')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000031', N'CH0000000003', N'各井筒矿石提升量', N'进风井', N'吨', N'0')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000032', N'CH0000000003', N'各井筒矿石提升量', N'原进风井', N'吨', N'3800')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000033', N'CH0000000003', N'各溜井出矿量', N'主溜井', N'吨', N'34931.248')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000034', N'CH0000000003', N'各溜井出矿量', N'6102溜井', N'吨', N'28300')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000035', N'CH0000000003', N'各溜井出矿量', N'7101溜井', N'吨', N'17355.772')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000036', N'CH0000000003', N'各溜井出矿量', N'6103溜井', N'吨', N'20100')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000037', N'CH0000000003', N'各溜井出矿量', N'7201溜井', N'吨', N'900')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000038', N'CH0000000003', N'各溜井出矿量', N'6104溜井', N'吨', N'10900')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000039', N'CH0000000003', N'废 石 量', N'', N'吨', N'17935')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000040', N'CH0000000003', N'各井筒废石提升量', N'主井', N'吨', N'10743.264')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000041', N'CH0000000003', N'各井筒废石提升量', N'日均车数', N'吨', N'304.514285714286')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000042', N'CH0000000003', N'各井筒废石提升量', N'副井', N'吨', N'')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000043', N'CH0000000003', N'各井筒废石提升量', N'日均车数', N'吨', N'')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000044', N'CH0000000003', N'各井筒废石提升量', N'进风井', N'吨', N'15127.624')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000045', N'CH0000000003', N'各井筒废石提升量', N'日均车数', N'吨', N'480.242031746032')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000046', N'CH0000000003', N'各井筒废石提升量', N'原进风井', N'吨', N'1421.55')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000047', N'CH0000000003', N'各井筒废石提升量', N'日均车数', N'吨', N'140')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000048', N'CH0000000003', N'掘进钻', N'总计', N'台', N'23')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000049', N'CH0000000003', N'掘进钻', N'二井', N'台', N'11')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000050', N'CH0000000003', N'掘进钻', N'项目', N'台', N'3')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000051', N'CH0000000003', N'掘进钻', N'温建', N'台', N'9')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000052', N'CH0000000003', N'地质钻', N'', N'米', N'1496')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000053', N'CH0000000003', N'潜孔钻', N'二井', N'米', N'6768')
INSERT [dbo].[T6_Check_B1_ZongBiao] ([ID], [CID], [ZB1], [ZB2], [DW], [BYYS]) VALUES (N'ZB0000000054', N'CH0000000003', N'潜孔钻', N'温建', N'米', N'7800')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000001', N'CH0000000003', N'采 掘 总 量', N'', N't', N'2125449', N'500594', N'23.55%', N'', N'609574', N'28.68%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000002', N'CH0000000003', N'采 掘 总 量', N'采掘一车间', N't', N'1256345', N'356308', N'28.36%', N'', N'413465', N'32.91%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000003', N'CH0000000003', N'采 掘 总 量', N'采掘二车间', N't', N'869104', N'144285', N'16.60%', N'', N'196109', N'22.56%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000004', N'CH0000000003', N'采  矿  量', N'', N't', N'1922209', N'435919', N'22.68%', N'', N'519219', N'27.01%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000005', N'CH0000000003', N'采  矿  量', N'采掘一车间', N'', N'1193385', N'331685', N'27.79%', N'', N'378485', N'31.72%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000006', N'CH0000000003', N'采  矿  量', N'采掘二车间', N't', N'728824', N'104234', N'14.30%', N'', N'140734', N'19.31%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000007', N'CH0000000003', N'掘  进  量', N'', N't', N'203240', N'64674', N'31.82%', N'', N'90354', N'44.46%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000008', N'CH0000000003', N'掘  进  量', N'采掘一车间', N't', N'62960', N'24623', N'39.11%', N'', N'34980', N'55.56%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000009', N'CH0000000003', N'掘  进  量', N'采掘二车间', N't', N'140280', N'40051', N'28.55%', N'', N'55374', N'39.47%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000010', N'CH0000000003', N'掘进标准米', N'', N'm', N'26930', N'4878', N'18.11%', N'', N'7089', N'26.32%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000011', N'CH0000000003', N'掘进自然米', N'', N'm', N'13568', N'2676', N'19.72%', N'', N'3929', N'28.95%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000012', N'CH0000000003', N'掘进自然米', N'地探', N'm', N'4889', N'864', N'17.68%', N'', N'1268', N'25.95%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000013', N'CH0000000003', N'掘进自然米', N'生探', N'm', N'475', N'233', N'49.09%', N'', N'233', N'49.09%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000014', N'CH0000000003', N'掘进自然米', N'开拓', N'm', N'4254', N'727', N'17.10%', N'', N'1173', N'27.58%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000015', N'CH0000000003', N'掘进自然米', N'采准', N'm', N'3429', N'647', N'18.86%', N'', N'959', N'27.97%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000016', N'CH0000000003', N'掘进自然米', N'切割', N'm', N'522', N'204', N'39.18%', N'', N'295', N'56.49%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000017', N'CH0000000003', N'供  矿  量', N'', N't', N'1675000', N'405406', N'24.20%', N'', N'535451', N'31.97%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000018', N'CH0000000003', N'矿岩提升量', N'总 量', N't', N'1842961', N'453174', N'24.59%', N'', N'610512', N'33.13%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000019', N'CH0000000003', N'矿岩提升量', N'废 石', N't', N'167961', N'54489', N'32.44%', N'', N'81781', N'48.69%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000020', N'CH0000000003', N'矿岩提升量', N'矿石量', N't', N'1675000', N'398686', N'23.80%', N'', N'528731', N'31.57%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000021', N'CH0000000003', N'潜  孔  钻', N'', N'm', N'206200', N'38779', N'18.81%', N'', N'53347', N'25.87%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000022', N'CH0000000003', N'坑 内 地 质 钻', N'', N'm', N'19010', N'3722', N'19.58%', N'', N'5218', N'27.45%')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000023', N'CH0000000003', N'万吨采掘比', N'', N'标准米/万吨', N'140.1', N'111.9', N'', N'', N'136.5', N'')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000024', N'CH0000000003', N'采矿损失率', N'', N'%', N'8.84', N'6.58', N'', N'', N'7.18', N'')
INSERT [dbo].[T6_Check_B3_CaiJue] ([ID], [CID], [ZB1], [ZB2], [DW], [NDYS], [YJWC1], [WCL1], [BYYS], [YJWC2], [WCL2]) VALUES (N'CJ0000000025', N'CH0000000003', N'矿石贫化率', N'', N'%', N'14.07', N'9.25', N'', N'', N'9.06', N'')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000001', N'CH0000000003', N'955m', N'', N'联巷压顶', N'切割', N'2.0', N'7', N'2.5*2.6', N'6.08', N'', N'60.0', N'162', N'15', N'', N'3.24—3.30', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000002', N'CH0000000003', N'955m', N'', N'联巷', N'采准', N'2.0', N'3', N'2.5*2.6', N'6.08', N'5', N'30', N'82', N'8', N'', N'3.21—3.23', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000003', N'CH0000000003', N'955m', N'', N'采准天井', N'采准', N'1.5', N'7', N'1.5×1.8', N'2.70', N'10', N'27', N'73', N'7', N'', N'3.21—3.27', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000004', N'CH0000000003', N'955m', N'', N'采准天井联络巷', N'采准', N'2.0', N'3', N'1.5×1.8', N'5.35', N'5', N'27', N'72', N'7', N'', N'3.28-3.30', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000005', N'CH0000000003', N'955m', N'', N'矿石溜井', N'采准', N'1.5', N'2', N'1.5*1.8', N'2.7', N'3', N'8', N'22', N'2', N'', N'4.2-4.3', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000006', N'CH0000000003', N'955m', N'', N'矿石溜井联络巷', N'采准', N'1.5', N'2', N'1.5*1.8', N'2.7', N'3.5', N'9', N'26', N'2', N'', N'3.31-4.1', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000007', N'CH0000000003', N'955m', N'', N'941m水平联络巷', N'采准', N'1.5', N'20', N'2.8×2.8', N'7.28', N'30', N'218', N'590', N'55', N'', N'3.31-4.20', N'③')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000008', N'CH0000000003', N'955m', N'', N'顶柱斜坡道', N'开拓', N'2.0', N'30', N'2.5*2.6', N'6.08', N'60', N'365', N'985', N'91', N'', N'3.31-4.20', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000009', N'CH0000000003', N'905m', N'', N'★北东翼探矿巷', N'地探', N'2.0', N'40', N'2.6×2.5', N'6.08', N'100', N'608', N'1642', N'152', N'', N'全月', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000010', N'CH0000000003', N'905m', N'', N'22线探矿巷', N'地探', N'2.0', N'30', N'2.6×2.5', N'6.08', N'60', N'365', N'985', N'91', N'', N'全月', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000011', N'CH0000000003', N'905m', N'', N'5#矿体斜坡道', N'开拓', N'2.0', N'10', N'2.8*2.8', N'7.28', N'20', N'146', N'393', N'36', N'', N'4.10-4.20', N'③')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000012', N'CH0000000003', N'905m', N'', N'4112采场切割天井', N'切割', N'1.5', N'7', N'1.5×1.8', N'2.70', N'10', N'27', N'97', N'7', N'97', N'3.31-4.3', N'④')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000013', N'CH0000000003', N'905m', N'', N'4112采场切割巷', N'切割', N'1.5', N'6', N'2.8×2.8', N'7.28', N'9', N'66', N'236', N'16', N'236', N'3.31-4.3', N'④')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000014', N'CH0000000003', N'855m ', N'', N'14-16线探矿巷', N'地探', N'2.0', N'25', N'2.8×2.7', N'7.00', N'50', N'350', N'1260', N'88', N'1260', N'全月', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000015', N'CH0000000003', N'855m ', N'', N'3#矿体2-3线探矿巷', N'地探', N'2.0', N'30', N'2.6×2.5', N'6.08', N'60', N'365', N'1313', N'91', N'1138', N'全月', N'③')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000016', N'CH0000000003', N'855m ', N'', N'★下盘探矿巷', N'开拓', N'2.0', N'50', N'2.6×2.5', N'6.08', N'100', N'608', N'1642', N'152', N'', N'全月', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000017', N'CH0000000003', N'805m', N'', N'8线穿脉', N'地探', N'2.0', N'7', N'2.8×2.8', N'7.28', N'14', N'102', N'275', N'25', N'', N'3.21—3.31', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000018', N'CH0000000003', N'805m', N'', N'6111矿柱采准天井', N'采准', N'1.5', N'28', N'1.5×1.8', N'2.70', N'42', N'113', N'306', N'28', N'', N'全月', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000019', N'CH0000000003', N'805m', N'', N'6线混凝土充填钻孔联络巷', N'开拓', N'1.5', N'7', N'2.8×2.8', N'7.28', N'10', N'73', N'197', N'18', N'', N'4.1—4.6', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000020', N'CH0000000003', N'755m', N'', N'7125采场切割井', N'切割', N'1.5', N'8', N'1.5×1.8', N'2.70', N'20', N'54', N'194', N'14', N'194', N'4.1—4.20', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000021', N'CH0000000003', N'755m', N'', N'7125采场切割巷', N'切割', N'1.5', N'8', N'2.8×2.8', N'7.28', N'7', N'51', N'183', N'13', N'183', N'4.1—4.20', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000022', N'CH0000000003', N'755m', N'', N'2#矿体0-1线沿脉巷', N'采准', N'2.0', N'10', N'2.8×2.8', N'7.28', N'20', N'146', N'379', N'36', N'', N'3.21—3.31', N'①')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000023', N'CH0000000003', N'755m', N'', N'7123顶柱切割井', N'切割', N'1.0', N'4', N'1.5×1.8', N'2.70', N'4.3', N'12', N'42', N'3', N'42', N'3.21—3.24', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000024', N'CH0000000003', N'755m', N'', N'7123顶柱凿岩巷', N'采准', N'1.0', N'15', N'2.8×2.8', N'7.28', N'15', N'109', N'393', N'27', N'393', N'3.25—4.8', N'②')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000025', N'CH0000000003', N'755m', N'', N'3线充填井（768m-793m）', N'采准', N'1.5', N'17', N'1.5×1.8', N'2.70', N'25', N'68', N'243', N'17', N'243', N'全月', N'③')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000026', N'CH0000000003', N'705m', N'', N'13线沉淀池', N'开拓', N'2.0', N'15', N'2.8×2.8', N'7.28', N'30', N'218', N'590', N'55', N'', N'4.8—4.20', N'④')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000027', N'CH0000000003', N'705m', N'', N'6线混凝土充填钻孔联络巷', N'开拓', N'2.0', N'8', N'2.8×2.8', N'7.28', N'16', N'116', N'314', N'29', N'', N'3.25—4.7', N'⑤')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000028', N'CH0000000003', N'705m', N'', N'2线采准井', N'采准', N'1.0', N'25', N'1.5×1.8', N'2.70', N'25', N'68', N'243', N'17', N'243', N'4.8—4.20', N'⑤')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000029', N'CH0000000003', N'705m', N'', N'13-21线盘区分段巷', N'采准', N'2.0', N'25', N'3.55×3.35', N'12.06', N'50', N'603', N'1628', N'151', N'', N'全月', N'⑥')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000030', N'CH0000000003', N'705m', N'', N'13-21线盘区运输巷', N'采准', N'3.0', N'17', N'2.8×2.85', N'8.08', N'50', N'404', N'1091', N'101', N'', N'3.21—4.7', N'④')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000031', N'CH0000000003', N'705m', N'', N'13-21线盘区采准井联络巷', N'采准', N'3.0', N'1', N'2.8×2.7', N'8.08', N'4', N'32', N'87', N'8', N'', N'3.21—3.23', N'⑦')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000032', N'CH0000000003', N'705m', N'', N'13-21线盘区充填井', N'采准', N'1.0', N'25', N'1.5×1.8', N'2.70', N'25', N'68', N'243', N'17', N'', N'4.1—4.20', N'⑦')
INSERT [dbo].[T6_Check_B4_JueJin] ([ID], [CID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000033', N'CH0000000003', N'705m', N'', N'13-21线切割巷', N'切割', N'3.0', N'13', N'2.8×2.8', N'7.28', N'40', N'291', N'1048', N'73', N'1048', N'全月', N'⑧')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000001', N'CH0000000003', N'955m', N'3212采场', N'充填', N'2.72', N'21.34', N'0.04', N'0.04', N'800', N'458', N'', N'458', N'2018.4.3', N'2018.4.7', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000002', N'CH0000000003', N'955m', N'3110采场', N'充填', N'2.25', N'20.63', N'0.06', N'0.04', N'2000', N'613', N'', N'613', N'2018.3.21', N'2018.3.31', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000003', N'CH0000000003', N'905m', N'4110采场（41-4）', N'浅采', N'1.61', N'27.63', N'0.09', N'0.02', N'6000', N'0', N'', N'', N'', N'', N'4.1开始回采')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000004', N'CH0000000003', N'855m', N'5123矿柱', N'中采', N'1.30', N'29.28', N'0.03', N'0.02', N'5000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000005', N'CH0000000003', N'855m', N'5127矿房', N'中采', N'1.33', N'29.28', N'0.05', N'0.09', N'8000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000006', N'CH0000000003', N'855m', N'5202矿柱', N'中采', N'1.27', N'27.08', N'0.06', N'0.02', N'4000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000007', N'CH0000000003', N'855m', N'5203矿柱', N'中采', N'1.90', N'27.04', N'0.09', N'0.07', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000008', N'CH0000000003', N'855m', N'5205矿柱', N'中采', N'1.90', N'27.04', N'0.09', N'0.07', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000009', N'CH0000000003', N'855m', N'5208采场', N'充填', N'1.75', N'25.08', N'0.06', N'0.03', N'500', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000010', N'CH0000000003', N'805m', N'6107矿房', N'中采', N'2.24', N'26.79', N'0.07', N'0.05', N'20000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000011', N'CH0000000003', N'805m', N'6111采场', N'充填', N'2.53', N'23.25', N'0.03', N'0.03', N'200', N'639', N'135', N'504', N'2018.4.10', N'2018.4.10', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000012', N'CH0000000003', N'805m', N'6117矿柱', N'中采', N'1.40', N'25.98', N'0.05', N'0.05', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000013', N'CH0000000003', N'805m', N'6123矿房', N'中采', N'1.35', N'27.78', N'0.03', N'0.04', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000014', N'CH0000000003', N'805m', N'6201采场', N'切采', N'2.06', N'25.97', N'0.08', N'0.05', N'300', N'1480', N'500', N'980', N'2018.4.4', N'2018.4.9', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000015', N'CH0000000003', N'805m', N'6205采场', N'', N'', N'', N'', N'', N'', N'1225', N'245', N'980', N'2018.4.12', N'2018.4.15', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000016', N'CH0000000003', N'755m', N'7113矿房', N'中采', N'2.50', N'22.35', N'0.07', N'0.06', N'5000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000017', N'CH0000000003', N'755m', N'7121矿房', N'中采', N'0.41', N'27.27', N'0.10', N'0.04', N'15000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000018', N'CH0000000003', N'755m', N'7115矿房', N'中采', N'3.04', N'17.12', N'0.13', N'0.12', N'1000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000019', N'CH0000000003', N'755m', N'7211矿房', N'中采', N'2.26', N'23.87', N'0.72', N'0.03', N'8000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000020', N'CH0000000003', N'755m', N'7203采场（切采）', N'切采', N'2.40', N'23.71', N'0.54', N'0.03', N'500', N'2393', N'', N'2393', N'2018.4.10', N'2018.4.20', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000021', N'CH0000000003', N'705m', N'13线-21线', N'切采', N'1.56', N'19.23', N'0.28', N'0.04', N'5000', N'1500', N'', N'1500', N'2018.4.10', N'2018.4.20', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000022', N'CH0000000003', N'705m', N'0线-4线切采（片帮）', N'切采', N'1.58', N'17.53', N'0.01', N'0.06', N'2000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Check_B6_CaiKuang] ([ID], [CID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000023', N'CH0000000003', N'705m', N'8205采场', N'', N'', N'', N'', N'', N'', N'1300', N'', N'1300', N'2018.4.15', N'2018.4.20', N'')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000001', N'CH0000000003', N'955m', N'3212采场', N'784', N'2.72', N'21.34', N'0.04', N'0.04', N'3', N'5', N'2.64', N'20.70', N'800')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000002', N'CH0000000003', N'955m', N'3110采场', N'979', N'1.61', N'27.63', N'0.09', N'0.02', N'3', N'5', N'1.56', N'26.80', N'1000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000003', N'CH0000000003', N'955m', N'3110采场（31-4）存隆', N'2000', N'2.25', N'20.63', N'0.06', N'0.04', N'10', N'10', N'2.03', N'18.57', N'2000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000004', N'CH0000000003', N'905m', N'4111矿柱', N'3000', N'1.28', N'29.27', N'0.01', N'0.01', N'10', N'10', N'1.15', N'26.34', N'3000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000005', N'CH0000000003', N'905m', N'4110采场（41-4）', N'1469', N'1.61', N'27.63', N'0.09', N'0.02', N'3', N'5', N'1.56', N'26.80', N'1500')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000006', N'CH0000000003', N'905m', N'8-12线1#矿体', N'0', N'1.61', N'27.63', N'0.09', N'0.02', N'3', N'5', N'1.56', N'26.80', N'0')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000007', N'CH0000000003', N'905m', N'副产', N'333', N'0.00', N'0.00', N'0.00', N'0.00', N'0', N'0', N'0.00', N'0.00', N'333')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000008', N'CH0000000003', N'855m', N'5123矿柱', N'3000', N'1.30', N'29.28', N'0.03', N'0.02', N'10', N'10', N'1.17', N'26.35', N'3000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000009', N'CH0000000003', N'855m', N'5127矿房', N'0', N'1.33', N'29.28', N'0.05', N'0.09', N'10', N'10', N'1.20', N'26.35', N'0')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000010', N'CH0000000003', N'855m', N'5202矿柱', N'3000', N'1.27', N'27.08', N'0.06', N'0.02', N'10', N'10', N'1.14', N'24.37', N'3000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000011', N'CH0000000003', N'855m', N'5203矿柱', N'10600', N'1.90', N'27.04', N'0.09', N'0.07', N'10', N'10', N'1.71', N'24.34', N'10600')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000012', N'CH0000000003', N'855m', N'5205矿柱', N'10600', N'1.90', N'28.50', N'0.08', N'0.13', N'10', N'10', N'1.71', N'25.65', N'10600')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000013', N'CH0000000003', N'855m', N'5208采场', N'490', N'1.75', N'25.08', N'0.06', N'0.03', N'3', N'5', N'1.70', N'24.33', N'500')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000014', N'CH0000000003', N'855m', N'副产', N'', N'1.67', N'25.10', N'0.05', N'0.02', N'', N'', N'1.67', N'25.10', N'2398')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000015', N'CH0000000003', N'805m', N'6107矿房', N'27391', N'2.24', N'26.79', N'0.07', N'0.05', N'8', N'10', N'2.06', N'24.65', N'28000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000016', N'CH0000000003', N'805m', N'6111采场', N'196', N'2.53', N'23.25', N'0.03', N'0.03', N'3', N'5', N'2.45', N'22.55', N'200')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000017', N'CH0000000003', N'805m', N'6117矿柱', N'9000', N'1.40', N'25.98', N'0.05', N'0.05', N'10', N'10', N'1.26', N'23.38', N'9000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000018', N'CH0000000003', N'805m', N'6119矿柱', N'21800', N'1.65', N'27.98', N'0.14', N'0.05', N'10', N'10', N'1.49', N'25.18', N'21800')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000019', N'CH0000000003', N'805m', N'6123矿房', N'0', N'1.35', N'27.78', N'0.03', N'0.04', N'10', N'10', N'1.22', N'25.00', N'')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000020', N'CH0000000003', N'805m', N'6201采场', N'300', N'2.06', N'25.97', N'0.08', N'0.05', N'0', N'0', N'2.06', N'25.97', N'300')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000021', N'CH0000000003', N'805m', N'副产', N'0', N'', N'', N'', N'', N'0', N'0', N'0.00', N'0.00', N'0')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000022', N'CH0000000003', N'755m', N'7113矿房', N'5870', N'2.50', N'22.35', N'0.07', N'0.06', N'8', N'10', N'2.30', N'20.56', N'6000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000023', N'CH0000000003', N'755m', N'7121矿房', N'2935', N'0.41', N'27.27', N'0.10', N'0.04', N'8', N'10', N'0.38', N'25.09', N'3000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000024', N'CH0000000003', N'755m', N'7211矿房', N'6500', N'2.26', N'23.87', N'0.72', N'0.03', N'10', N'10', N'2.03', N'21.48', N'6500')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000025', N'CH0000000003', N'755m', N'7115矿房', N'800', N'3.04', N'17.12', N'0.13', N'0.12', N'10', N'10', N'2.74', N'15.41', N'800')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000026', N'CH0000000003', N'755m', N'7117矿房', N'3000', N'2.31', N'24.70', N'0.19', N'0.12', N'10', N'10', N'2.07', N'22.23', N'3000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000027', N'CH0000000003', N'755m', N'7119矿房', N'1000', N'0.46', N'128.40', N'0.04', N'0.03', N'10', N'10', N'0.41', N'115.56', N'1000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000028', N'CH0000000003', N'755m', N'7207矿房', N'800', N'2.09', N'21.03', N'0.31', N'0.01', N'10', N'10', N'1.88', N'18.93', N'800')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000029', N'CH0000000003', N'755m', N'7203采场（切采）', N'600', N'2.40', N'23.71', N'0.54', N'0.03', N'0', N'0', N'2.40', N'23.71', N'600')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000030', N'CH0000000003', N'755m', N'7209矿房堆存副产', N'100', N'1.62', N'22.00', N'0.38', N'0.07', N'0', N'0', N'1.62', N'22.00', N'100')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000031', N'CH0000000003', N'755m', N'副产', N'1056', N'1.40', N'21.26', N'0.25', N'0.18', N'0', N'0', N'1.40', N'21.26', N'1056')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000032', N'CH0000000003', N'705m', N'13线-21线切采', N'5000', N'1.56', N'21.58', N'0.48', N'0.05', N'0', N'0', N'1.56', N'21.58', N'5000')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000033', N'CH0000000003', N'705m', N'0线-4线切采（片帮）', N'0', N'1.58', N'17.53', N'0.01', N'0.06', N'0', N'0', N'1.58', N'17.53', N'')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000034', N'CH0000000003', N'705m', N'2#矿体3-9线切采（片帮）', N'200', N'1.17', N'23.58', N'0.05', N'0.02', N'0', N'0', N'1.17', N'23.58', N'200')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000035', N'CH0000000003', N'705m', N'705m副产', N'1291', N'1.64', N'19.48', N'0.40', N'0.05', N'0', N'0', N'1.64', N'19.48', N'1291')
INSERT [dbo].[T6_Check_B7_ChuKuang] ([ID], [CID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000036', N'CH0000000003', N'705m', N'666m副产', N'2667', N'1.30', N'23.99', N'0.17', N'0.06', N'0', N'0', N'1.30', N'23.99', N'2667')
INSERT [dbo].[T6_Plan] ([ID], [YM], [FileName], [FileNameS], [UploadTime]) VALUES (N'PL0000000002', N'2018/12', N'2018年12月份生产计划最终.xlsx', N'f48791f0-3787-4263-a7e8-af4ddda0dc90.xlsx', CAST(N'2018-12-26T09:55:55.000' AS DateTime))
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000001', N'PL0000000002', N'日历天数', N'', N'天', N'31')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000002', N'PL0000000002', N'3000吨/日选厂', N'生产', N'天', N'25')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000003', N'PL0000000002', N'3000吨/日选厂', N'检修', N'天', N'6')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000004', N'PL0000000002', N'2000吨/日选厂', N'生产', N'天', N'29')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000005', N'PL0000000002', N'2000吨/日选厂', N'检修', N'天', N'2')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000006', N'PL0000000002', N'箕斗井', N'生产', N'天', N'25')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000007', N'PL0000000002', N'箕斗井', N'检修', N'天', N'6')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000008', N'PL0000000002', N'采掘总量', N'', N'吨', N'108980')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000009', N'PL0000000002', N'采掘总量', N'采掘车间', N'吨', N'6000')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000010', N'PL0000000002', N'采掘总量', N'温州建设', N'吨', N'46346')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000011', N'PL0000000002', N'采掘总量', N'温州二井', N'吨', N'47990')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000012', N'PL0000000002', N'采掘总量', N'项目工程', N'吨', N'8644')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000013', N'PL0000000002', N'采矿量', N'', N'吨', N'83300')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000014', N'PL0000000002', N'掘进量', N'', N'吨', N'25680')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000015', N'PL0000000002', N'掘进米', N'', N'自然米', N'1253')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000016', N'PL0000000002', N'掘进米', N'', N'标准米', N'2211')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000017', N'PL0000000002', N'充填量', N'', N'立方米', N'9608')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000018', N'PL0000000002', N'选矿处理量', N'', N'吨', N'130450')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000019', N'PL0000000002', N'锌入选品位', N'', N'%', N'1.69')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000020', N'PL0000000002', N'铁入选品位', N'', N'%', N'24.08')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000021', N'PL0000000002', N'铜入选品位', N'', N'%', N'0.13')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000022', N'PL0000000002', N'锌综合回收率', N'', N'%', N'92.00')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000023', N'PL0000000002', N'铁综合回收率', N'', N'%', N'45.00')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000024', N'PL0000000002', N'铜综合回收率', N'', N'%', N'65.00')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000025', N'PL0000000002', N'锌金属产量', N'', N'吨', N'2100')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000026', N'PL0000000002', N'铁精矿产量', N'', N'吨', N'21422')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000027', N'PL0000000002', N'铜金属产量', N'', N'吨', N'113')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000028', N'PL0000000002', N'井下供矿', N'', N'吨', N'130045')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000029', N'PL0000000002', N'各井筒矿石提升量', N'箕斗井', N'吨', N'126244.86')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000030', N'PL0000000002', N'各井筒矿石提升量', N'日均提升', N'吨', N'5049.7944')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000031', N'PL0000000002', N'各井筒矿石提升量', N'进风井', N'吨', N'0')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000032', N'PL0000000002', N'各井筒矿石提升量', N'原进风井', N'吨', N'3800')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000033', N'PL0000000002', N'各溜井出矿量', N'主溜井', N'吨', N'34931.248')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000034', N'PL0000000002', N'各溜井出矿量', N'6102溜井', N'吨', N'28300')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000035', N'PL0000000002', N'各溜井出矿量', N'7101溜井', N'吨', N'17355.772')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000036', N'PL0000000002', N'各溜井出矿量', N'6103溜井', N'吨', N'20100')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000037', N'PL0000000002', N'各溜井出矿量', N'7201溜井', N'吨', N'900')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000038', N'PL0000000002', N'各溜井出矿量', N'6104溜井', N'吨', N'10900')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000039', N'PL0000000002', N'废 石 量', N'', N'吨', N'17935')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000040', N'PL0000000002', N'各井筒废石提升量', N'主井', N'吨', N'10743.264')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000041', N'PL0000000002', N'各井筒废石提升量', N'日均车数', N'吨', N'304.514285714286')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000042', N'PL0000000002', N'各井筒废石提升量', N'副井', N'吨', N'')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000043', N'PL0000000002', N'各井筒废石提升量', N'日均车数', N'吨', N'')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000044', N'PL0000000002', N'各井筒废石提升量', N'进风井', N'吨', N'15127.624')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000045', N'PL0000000002', N'各井筒废石提升量', N'日均车数', N'吨', N'480.242031746032')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000046', N'PL0000000002', N'各井筒废石提升量', N'原进风井', N'吨', N'1421.55')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000047', N'PL0000000002', N'各井筒废石提升量', N'日均车数', N'吨', N'140')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000048', N'PL0000000002', N'掘进钻', N'总计', N'台', N'23')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000049', N'PL0000000002', N'掘进钻', N'二井', N'台', N'11')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000050', N'PL0000000002', N'掘进钻', N'项目', N'台', N'3')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000051', N'PL0000000002', N'掘进钻', N'温建', N'台', N'9')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000052', N'PL0000000002', N'地质钻', N'', N'米', N'1496')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000053', N'PL0000000002', N'潜孔钻', N'二井', N'米', N'6768')
INSERT [dbo].[T6_Plan_B1_ZongBiao] ([ID], [PID], [ZB1], [ZB2], [DW], [BYJH]) VALUES (N'ZB0000000054', N'PL0000000002', N'潜孔钻', N'温建', N'米', N'7800')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000001', N'PL0000000002', N'采 掘 总 量', N'', N't', N'2125449', N'500594', N'23.55%', N'', N'609574', N'28.68%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000002', N'PL0000000002', N'采 掘 总 量', N'采掘一车间', N't', N'1256345', N'356308', N'28.36%', N'', N'413465', N'32.91%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000003', N'PL0000000002', N'采 掘 总 量', N'采掘二车间', N't', N'869104', N'144285', N'16.60%', N'', N'196109', N'22.56%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000004', N'PL0000000002', N'采  矿  量', N'', N't', N'1922209', N'435919', N'22.68%', N'', N'519219', N'27.01%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000005', N'PL0000000002', N'采  矿  量', N'采掘一车间', N'', N'1193385', N'331685', N'27.79%', N'', N'378485', N'31.72%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000006', N'PL0000000002', N'采  矿  量', N'采掘二车间', N't', N'728824', N'104234', N'14.30%', N'', N'140734', N'19.31%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000007', N'PL0000000002', N'掘  进  量', N'', N't', N'203240', N'64674', N'31.82%', N'', N'90354', N'44.46%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000008', N'PL0000000002', N'掘  进  量', N'采掘一车间', N't', N'62960', N'24623', N'39.11%', N'', N'34980', N'55.56%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000009', N'PL0000000002', N'掘  进  量', N'采掘二车间', N't', N'140280', N'40051', N'28.55%', N'', N'55374', N'39.47%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000010', N'PL0000000002', N'掘进标准米', N'', N'm', N'26930', N'4878', N'18.11%', N'', N'7089', N'26.32%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000011', N'PL0000000002', N'掘进自然米', N'', N'm', N'13568', N'2676', N'19.72%', N'', N'3929', N'28.95%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000012', N'PL0000000002', N'掘进自然米', N'地探', N'm', N'4889', N'864', N'17.68%', N'', N'1268', N'25.95%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000013', N'PL0000000002', N'掘进自然米', N'生探', N'm', N'475', N'233', N'49.09%', N'', N'233', N'49.09%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000014', N'PL0000000002', N'掘进自然米', N'开拓', N'm', N'4254', N'727', N'17.10%', N'', N'1173', N'27.58%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000015', N'PL0000000002', N'掘进自然米', N'采准', N'm', N'3429', N'647', N'18.86%', N'', N'959', N'27.97%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000016', N'PL0000000002', N'掘进自然米', N'切割', N'm', N'522', N'204', N'39.18%', N'', N'295', N'56.49%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000017', N'PL0000000002', N'供  矿  量', N'', N't', N'1675000', N'405406', N'24.20%', N'', N'535451', N'31.97%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000018', N'PL0000000002', N'矿岩提升量', N'总 量', N't', N'1842961', N'453174', N'24.59%', N'', N'610512', N'33.13%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000019', N'PL0000000002', N'矿岩提升量', N'废 石', N't', N'167961', N'54489', N'32.44%', N'', N'81781', N'48.69%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000020', N'PL0000000002', N'矿岩提升量', N'矿石量', N't', N'1675000', N'398686', N'23.80%', N'', N'528731', N'31.57%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000021', N'PL0000000002', N'潜  孔  钻', N'', N'm', N'206200', N'38779', N'18.81%', N'', N'53347', N'25.87%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000022', N'PL0000000002', N'坑 内 地 质 钻', N'', N'm', N'19010', N'3722', N'19.58%', N'', N'5218', N'27.45%')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000023', N'PL0000000002', N'万吨采掘比', N'', N'标准米/万吨', N'140.1', N'111.9', N'', N'', N'136.5', N'')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000024', N'PL0000000002', N'采矿损失率', N'', N'%', N'8.84', N'6.58', N'', N'', N'7.18', N'')
INSERT [dbo].[T6_Plan_B3_CaiJue] ([ID], [PID], [ZB1], [ZB2], [DW], [NDJH], [YJWC1], [WCL1], [BYJH], [YJWC2], [WCL2]) VALUES (N'CJ0000000025', N'PL0000000002', N'矿石贫化率', N'', N'%', N'14.07', N'9.25', N'', N'', N'9.06', N'')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000001', N'PL0000000002', N'955m', N'', N'联巷压顶', N'切割', N'2.0', N'7', N'2.5*2.6', N'6.08', N'', N'60.0', N'162', N'15', N'', N'3.24—3.30', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000002', N'PL0000000002', N'955m', N'', N'联巷', N'采准', N'2.0', N'3', N'2.5*2.6', N'6.08', N'5', N'30', N'82', N'8', N'', N'3.21—3.23', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000003', N'PL0000000002', N'955m', N'', N'采准天井', N'采准', N'1.5', N'7', N'1.5×1.8', N'2.70', N'10', N'27', N'73', N'7', N'', N'3.21—3.27', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000004', N'PL0000000002', N'955m', N'', N'采准天井联络巷', N'采准', N'2.0', N'3', N'1.5×1.8', N'5.35', N'5', N'27', N'72', N'7', N'', N'3.28-3.30', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000005', N'PL0000000002', N'955m', N'', N'矿石溜井', N'采准', N'1.5', N'2', N'1.5*1.8', N'2.7', N'3', N'8', N'22', N'2', N'', N'4.2-4.3', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000006', N'PL0000000002', N'955m', N'', N'矿石溜井联络巷', N'采准', N'1.5', N'2', N'1.5*1.8', N'2.7', N'3.5', N'9', N'26', N'2', N'', N'3.31-4.1', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000007', N'PL0000000002', N'955m', N'', N'941m水平联络巷', N'采准', N'1.5', N'20', N'2.8×2.8', N'7.28', N'30', N'218', N'590', N'55', N'', N'3.31-4.20', N'③')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000008', N'PL0000000002', N'955m', N'', N'顶柱斜坡道', N'开拓', N'2.0', N'30', N'2.5*2.6', N'6.08', N'60', N'365', N'985', N'91', N'', N'3.31-4.20', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000009', N'PL0000000002', N'905m', N'', N'★北东翼探矿巷', N'地探', N'2.0', N'40', N'2.6×2.5', N'6.08', N'100', N'608', N'1642', N'152', N'', N'全月', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000010', N'PL0000000002', N'905m', N'', N'22线探矿巷', N'地探', N'2.0', N'30', N'2.6×2.5', N'6.08', N'60', N'365', N'985', N'91', N'', N'全月', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000011', N'PL0000000002', N'905m', N'', N'5#矿体斜坡道', N'开拓', N'2.0', N'10', N'2.8*2.8', N'7.28', N'20', N'146', N'393', N'36', N'', N'4.10-4.20', N'③')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000012', N'PL0000000002', N'905m', N'', N'4112采场切割天井', N'切割', N'1.5', N'7', N'1.5×1.8', N'2.70', N'10', N'27', N'97', N'7', N'97', N'3.31-4.3', N'④')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000013', N'PL0000000002', N'905m', N'', N'4112采场切割巷', N'切割', N'1.5', N'6', N'2.8×2.8', N'7.28', N'9', N'66', N'236', N'16', N'236', N'3.31-4.3', N'④')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000014', N'PL0000000002', N'855m ', N'', N'14-16线探矿巷', N'地探', N'2.0', N'25', N'2.8×2.7', N'7.00', N'50', N'350', N'1260', N'88', N'1260', N'全月', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000015', N'PL0000000002', N'855m ', N'', N'3#矿体2-3线探矿巷', N'地探', N'2.0', N'30', N'2.6×2.5', N'6.08', N'60', N'365', N'1313', N'91', N'1138', N'全月', N'③')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000016', N'PL0000000002', N'855m ', N'', N'★下盘探矿巷', N'开拓', N'2.0', N'50', N'2.6×2.5', N'6.08', N'100', N'608', N'1642', N'152', N'', N'全月', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000017', N'PL0000000002', N'805m', N'', N'8线穿脉', N'地探', N'2.0', N'7', N'2.8×2.8', N'7.28', N'14', N'102', N'275', N'25', N'', N'3.21—3.31', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000018', N'PL0000000002', N'805m', N'', N'6111矿柱采准天井', N'采准', N'1.5', N'28', N'1.5×1.8', N'2.70', N'42', N'113', N'306', N'28', N'', N'全月', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000019', N'PL0000000002', N'805m', N'', N'6线混凝土充填钻孔联络巷', N'开拓', N'1.5', N'7', N'2.8×2.8', N'7.28', N'10', N'73', N'197', N'18', N'', N'4.1—4.6', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000020', N'PL0000000002', N'755m', N'', N'7125采场切割井', N'切割', N'1.5', N'8', N'1.5×1.8', N'2.70', N'20', N'54', N'194', N'14', N'194', N'4.1—4.20', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000021', N'PL0000000002', N'755m', N'', N'7125采场切割巷', N'切割', N'1.5', N'8', N'2.8×2.8', N'7.28', N'7', N'51', N'183', N'13', N'183', N'4.1—4.20', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000022', N'PL0000000002', N'755m', N'', N'2#矿体0-1线沿脉巷', N'采准', N'2.0', N'10', N'2.8×2.8', N'7.28', N'20', N'146', N'379', N'36', N'', N'3.21—3.31', N'①')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000023', N'PL0000000002', N'755m', N'', N'7123顶柱切割井', N'切割', N'1.0', N'4', N'1.5×1.8', N'2.70', N'4.3', N'12', N'42', N'3', N'42', N'3.21—3.24', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000024', N'PL0000000002', N'755m', N'', N'7123顶柱凿岩巷', N'采准', N'1.0', N'15', N'2.8×2.8', N'7.28', N'15', N'109', N'393', N'27', N'393', N'3.25—4.8', N'②')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000025', N'PL0000000002', N'755m', N'', N'3线充填井（768m-793m）', N'采准', N'1.5', N'17', N'1.5×1.8', N'2.70', N'25', N'68', N'243', N'17', N'243', N'全月', N'③')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000026', N'PL0000000002', N'705m', N'', N'13线沉淀池', N'开拓', N'2.0', N'15', N'2.8×2.8', N'7.28', N'30', N'218', N'590', N'55', N'', N'4.8—4.20', N'④')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000027', N'PL0000000002', N'705m', N'', N'6线混凝土充填钻孔联络巷', N'开拓', N'2.0', N'8', N'2.8×2.8', N'7.28', N'16', N'116', N'314', N'29', N'', N'3.25—4.7', N'⑤')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000028', N'PL0000000002', N'705m', N'', N'2线采准井', N'采准', N'1.0', N'25', N'1.5×1.8', N'2.70', N'25', N'68', N'243', N'17', N'243', N'4.8—4.20', N'⑤')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000029', N'PL0000000002', N'705m', N'', N'13-21线盘区分段巷', N'采准', N'2.0', N'25', N'3.55×3.35', N'12.06', N'50', N'603', N'1628', N'151', N'', N'全月', N'⑥')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000030', N'PL0000000002', N'705m', N'', N'13-21线盘区运输巷', N'采准', N'3.0', N'17', N'2.8×2.85', N'8.08', N'50', N'404', N'1091', N'101', N'', N'3.21—4.7', N'④')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000031', N'PL0000000002', N'705m', N'', N'13-21线盘区采准井联络巷', N'采准', N'3.0', N'1', N'2.8×2.7', N'8.08', N'4', N'32', N'87', N'8', N'', N'3.21—3.23', N'⑦')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000032', N'PL0000000002', N'705m', N'', N'13-21线盘区充填井', N'采准', N'1.0', N'25', N'1.5×1.8', N'2.70', N'25', N'68', N'243', N'17', N'', N'4.1—4.20', N'⑦')
INSERT [dbo].[T6_Plan_B4_JueJin] ([ID], [PID], [ZD], [CC], [ZYM], [GCLX], [TX], [TB], [GG], [DMJ], [CD], [TJ], [JJL], [ZHBM], [FC], [SGSJ], [JT]) VALUES (N'JJ0000000033', N'PL0000000002', N'705m', N'', N'13-21线切割巷', N'切割', N'3.0', N'13', N'2.8×2.8', N'7.28', N'40', N'291', N'1048', N'73', N'1048', N'全月', N'⑧')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000001', N'PL0000000002', N'955m', N'3212采场', N'充填', N'2.72', N'21.34', N'0.04', N'0.04', N'800', N'458', N'', N'458', N'2018.4.3', N'2018.4.7', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000002', N'PL0000000002', N'955m', N'3110采场', N'充填', N'2.25', N'20.63', N'0.06', N'0.04', N'2000', N'613', N'', N'613', N'2018.3.21', N'2018.3.31', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000003', N'PL0000000002', N'905m', N'4110采场（41-4）', N'浅采', N'1.61', N'27.63', N'0.09', N'0.02', N'6000', N'0', N'', N'', N'', N'', N'4.1开始回采')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000004', N'PL0000000002', N'855m', N'5123矿柱', N'中采', N'1.30', N'29.28', N'0.03', N'0.02', N'5000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000005', N'PL0000000002', N'855m', N'5127矿房', N'中采', N'1.33', N'29.28', N'0.05', N'0.09', N'8000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000006', N'PL0000000002', N'855m', N'5202矿柱', N'中采', N'1.27', N'27.08', N'0.06', N'0.02', N'4000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000007', N'PL0000000002', N'855m', N'5203矿柱', N'中采', N'1.90', N'27.04', N'0.09', N'0.07', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000008', N'PL0000000002', N'855m', N'5205矿柱', N'中采', N'1.90', N'27.04', N'0.09', N'0.07', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000009', N'PL0000000002', N'855m', N'5208采场', N'充填', N'1.75', N'25.08', N'0.06', N'0.03', N'500', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000010', N'PL0000000002', N'805m', N'6107矿房', N'中采', N'2.24', N'26.79', N'0.07', N'0.05', N'20000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000011', N'PL0000000002', N'805m', N'6111采场', N'充填', N'2.53', N'23.25', N'0.03', N'0.03', N'200', N'639', N'135', N'504', N'2018.4.10', N'2018.4.10', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000012', N'PL0000000002', N'805m', N'6117矿柱', N'中采', N'1.40', N'25.98', N'0.05', N'0.05', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000013', N'PL0000000002', N'805m', N'6123矿房', N'中采', N'1.35', N'27.78', N'0.03', N'0.04', N'', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000014', N'PL0000000002', N'805m', N'6201采场', N'切采', N'2.06', N'25.97', N'0.08', N'0.05', N'300', N'1480', N'500', N'980', N'2018.4.4', N'2018.4.9', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000015', N'PL0000000002', N'805m', N'6205采场', N'', N'', N'', N'', N'', N'', N'1225', N'245', N'980', N'2018.4.12', N'2018.4.15', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000016', N'PL0000000002', N'755m', N'7113矿房', N'中采', N'2.50', N'22.35', N'0.07', N'0.06', N'5000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000017', N'PL0000000002', N'755m', N'7121矿房', N'中采', N'0.41', N'27.27', N'0.10', N'0.04', N'15000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000018', N'PL0000000002', N'755m', N'7115矿房', N'中采', N'3.04', N'17.12', N'0.13', N'0.12', N'1000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000019', N'PL0000000002', N'755m', N'7211矿房', N'中采', N'2.26', N'23.87', N'0.72', N'0.03', N'8000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000020', N'PL0000000002', N'755m', N'7203采场（切采）', N'切采', N'2.40', N'23.71', N'0.54', N'0.03', N'500', N'2393', N'', N'2393', N'2018.4.10', N'2018.4.20', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000021', N'PL0000000002', N'705m', N'13线-21线', N'切采', N'1.56', N'19.23', N'0.28', N'0.04', N'5000', N'1500', N'', N'1500', N'2018.4.10', N'2018.4.20', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000022', N'PL0000000002', N'705m', N'0线-4线切采（片帮）', N'切采', N'1.58', N'17.53', N'0.01', N'0.06', N'2000', N'0', N'', N'', N'', N'', N'')
INSERT [dbo].[T6_Plan_B6_CaiKuang] ([ID], [PID], [ZD], [CC], [CKLX], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [CKL], [TCZL], [WSL], [JJL], [KSSJ], [JSSJ], [BZ]) VALUES (N'CK0000000023', N'PL0000000002', N'705m', N'8205采场', N'', N'', N'', N'', N'', N'', N'1300', N'', N'1300', N'2018.4.15', N'2018.4.20', N'')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000001', N'PL0000000002', N'955m', N'3212采场', N'784', N'2.72', N'21.34', N'0.04', N'0.04', N'3', N'5', N'2.64', N'20.70', N'800')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000002', N'PL0000000002', N'955m', N'3110采场', N'979', N'1.61', N'27.63', N'0.09', N'0.02', N'3', N'5', N'1.56', N'26.80', N'1000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000003', N'PL0000000002', N'955m', N'3110采场（31-4）存隆', N'2000', N'2.25', N'20.63', N'0.06', N'0.04', N'10', N'10', N'2.03', N'18.57', N'2000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000004', N'PL0000000002', N'905m', N'4111矿柱', N'3000', N'1.28', N'29.27', N'0.01', N'0.01', N'10', N'10', N'1.15', N'26.34', N'3000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000005', N'PL0000000002', N'905m', N'4110采场（41-4）', N'1469', N'1.61', N'27.63', N'0.09', N'0.02', N'3', N'5', N'1.56', N'26.80', N'1500')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000006', N'PL0000000002', N'905m', N'8-12线1#矿体', N'0', N'1.61', N'27.63', N'0.09', N'0.02', N'3', N'5', N'1.56', N'26.80', N'0')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000007', N'PL0000000002', N'905m', N'副产', N'333', N'0.00', N'0.00', N'0.00', N'0.00', N'0', N'0', N'0.00', N'0.00', N'333')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000008', N'PL0000000002', N'855m', N'5123矿柱', N'3000', N'1.30', N'29.28', N'0.03', N'0.02', N'10', N'10', N'1.17', N'26.35', N'3000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000009', N'PL0000000002', N'855m', N'5127矿房', N'0', N'1.33', N'29.28', N'0.05', N'0.09', N'10', N'10', N'1.20', N'26.35', N'0')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000010', N'PL0000000002', N'855m', N'5202矿柱', N'3000', N'1.27', N'27.08', N'0.06', N'0.02', N'10', N'10', N'1.14', N'24.37', N'3000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000011', N'PL0000000002', N'855m', N'5203矿柱', N'10600', N'1.90', N'27.04', N'0.09', N'0.07', N'10', N'10', N'1.71', N'24.34', N'10600')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000012', N'PL0000000002', N'855m', N'5205矿柱', N'10600', N'1.90', N'28.50', N'0.08', N'0.13', N'10', N'10', N'1.71', N'25.65', N'10600')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000013', N'PL0000000002', N'855m', N'5208采场', N'490', N'1.75', N'25.08', N'0.06', N'0.03', N'3', N'5', N'1.70', N'24.33', N'500')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000014', N'PL0000000002', N'855m', N'副产', N'', N'1.67', N'25.10', N'0.05', N'0.02', N'', N'', N'1.67', N'25.10', N'2398')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000015', N'PL0000000002', N'805m', N'6107矿房', N'27391', N'2.24', N'26.79', N'0.07', N'0.05', N'8', N'10', N'2.06', N'24.65', N'28000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000016', N'PL0000000002', N'805m', N'6111采场', N'196', N'2.53', N'23.25', N'0.03', N'0.03', N'3', N'5', N'2.45', N'22.55', N'200')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000017', N'PL0000000002', N'805m', N'6117矿柱', N'9000', N'1.40', N'25.98', N'0.05', N'0.05', N'10', N'10', N'1.26', N'23.38', N'9000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000018', N'PL0000000002', N'805m', N'6119矿柱', N'21800', N'1.65', N'27.98', N'0.14', N'0.05', N'10', N'10', N'1.49', N'25.18', N'21800')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000019', N'PL0000000002', N'805m', N'6123矿房', N'0', N'1.35', N'27.78', N'0.03', N'0.04', N'10', N'10', N'1.22', N'25.00', N'')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000020', N'PL0000000002', N'805m', N'6201采场', N'300', N'2.06', N'25.97', N'0.08', N'0.05', N'0', N'0', N'2.06', N'25.97', N'300')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000021', N'PL0000000002', N'805m', N'副产', N'0', N'', N'', N'', N'', N'0', N'0', N'0.00', N'0.00', N'0')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000022', N'PL0000000002', N'755m', N'7113矿房', N'5870', N'2.50', N'22.35', N'0.07', N'0.06', N'8', N'10', N'2.30', N'20.56', N'6000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000023', N'PL0000000002', N'755m', N'7121矿房', N'2935', N'0.41', N'27.27', N'0.10', N'0.04', N'8', N'10', N'0.38', N'25.09', N'3000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000024', N'PL0000000002', N'755m', N'7211矿房', N'6500', N'2.26', N'23.87', N'0.72', N'0.03', N'10', N'10', N'2.03', N'21.48', N'6500')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000025', N'PL0000000002', N'755m', N'7115矿房', N'800', N'3.04', N'17.12', N'0.13', N'0.12', N'10', N'10', N'2.74', N'15.41', N'800')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000026', N'PL0000000002', N'755m', N'7117矿房', N'3000', N'2.31', N'24.70', N'0.19', N'0.12', N'10', N'10', N'2.07', N'22.23', N'3000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000027', N'PL0000000002', N'755m', N'7119矿房', N'1000', N'0.46', N'128.40', N'0.04', N'0.03', N'10', N'10', N'0.41', N'115.56', N'1000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000028', N'PL0000000002', N'755m', N'7207矿房', N'800', N'2.09', N'21.03', N'0.31', N'0.01', N'10', N'10', N'1.88', N'18.93', N'800')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000029', N'PL0000000002', N'755m', N'7203采场（切采）', N'600', N'2.40', N'23.71', N'0.54', N'0.03', N'0', N'0', N'2.40', N'23.71', N'600')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000030', N'PL0000000002', N'755m', N'7209矿房堆存副产', N'100', N'1.62', N'22.00', N'0.38', N'0.07', N'0', N'0', N'1.62', N'22.00', N'100')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000031', N'PL0000000002', N'755m', N'副产', N'1056', N'1.40', N'21.26', N'0.25', N'0.18', N'0', N'0', N'1.40', N'21.26', N'1056')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000032', N'PL0000000002', N'705m', N'13线-21线切采', N'5000', N'1.56', N'21.58', N'0.48', N'0.05', N'0', N'0', N'1.56', N'21.58', N'5000')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000033', N'PL0000000002', N'705m', N'0线-4线切采（片帮）', N'0', N'1.58', N'17.53', N'0.01', N'0.06', N'0', N'0', N'1.58', N'17.53', N'')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000034', N'PL0000000002', N'705m', N'2#矿体3-9线切采（片帮）', N'200', N'1.17', N'23.58', N'0.05', N'0.02', N'0', N'0', N'1.17', N'23.58', N'200')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000035', N'PL0000000002', N'705m', N'705m副产', N'1291', N'1.64', N'19.48', N'0.40', N'0.05', N'0', N'0', N'1.64', N'19.48', N'1291')
INSERT [dbo].[T6_Plan_B7_ChuKuang] ([ID], [PID], [ZD], [CC], [XHKL], [DZPW_X], [DZPW_T], [DZPW_C], [DZPW_L], [PHL], [SSL], [CKPW_X], [CKPW_T], [CKL]) VALUES (N'CK0000000036', N'PL0000000002', N'705m', N'666m副产', N'2667', N'1.30', N'23.99', N'0.17', N'0.06', N'0', N'0', N'1.30', N'23.99', N'2667')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'A', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'B', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'C', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'D', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'E', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'F', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'G', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'H', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'I', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'J', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'K', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'L', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'M', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'N', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'O', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'P', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'Q', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'R', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'S', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T7_Sample] ([ID], [Code], [STime], [PID], [Sampler], [Memo], [Result], [RTime], [Analyst]) VALUES (N'T', N'2', CAST(N'2018-11-03T14:03:00.000' AS DateTime), N'3', N'4', N'5', N'6', CAST(N'2018-11-03T14:04:00.000' AS DateTime), N'7')
INSERT [dbo].[T8_ChuKuang] ([ID], [WorkDate], [WorkClassCode], [PositionCode], [EquipmentID], [W1], [W2]) VALUES (N'CK201812210000000001010001001001', N'2018-12-21', N'1', N'001001001', N'TE0000000001', CAST(18.00 AS Numeric(10, 2)), CAST(2.00 AS Numeric(10, 2)))
INSERT [dbo].[T8_ChuKuang] ([ID], [WorkDate], [WorkClassCode], [PositionCode], [EquipmentID], [W1], [W2]) VALUES (N'CK201812210000000001010001001002', N'2018-12-21', N'1', N'001001002', N'TE0000000001', CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)))
INSERT [dbo].[T8_ChuKuang] ([ID], [WorkDate], [WorkClassCode], [PositionCode], [EquipmentID], [W1], [W2]) VALUES (N'CK201812210000000001010001002001', N'2018-12-21', N'1', N'001002001', N'TE0000000001', CAST(18.00 AS Numeric(10, 2)), CAST(2.00 AS Numeric(10, 2)))
INSERT [dbo].[T8_ChuKuang] ([ID], [WorkDate], [WorkClassCode], [PositionCode], [EquipmentID], [W1], [W2]) VALUES (N'CK201812210000000001010004001001', N'2018-12-21', N'1', N'004001001', N'TE0000000001', CAST(54.00 AS Numeric(10, 2)), CAST(6.00 AS Numeric(10, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100001', N'2018', N'12', N'21', N'001', N'处理量t', 4, N'001001', N'采掘一车间', CAST(-200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100002', N'2018', N'12', N'21', N'001', N'处理量t', 4, N'001002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100003', N'2018', N'12', N'21', N'001', N'处理量t', 4, N'001003', N'地表', CAST(300.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100004', N'2018', N'12', N'21', N'001', N'处理量t', 4, N'001004', N'合计', CAST(100.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100005', N'2018', N'12', N'21', N'002', N'原矿金属量t', 4, N'002001', N'采掘一车间', CAST(-400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100006', N'2018', N'12', N'21', N'002', N'原矿金属量t', 4, N'002002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100007', N'2018', N'12', N'21', N'002', N'原矿金属量t', 4, N'002003', N'地表', CAST(600.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100008', N'2018', N'12', N'21', N'002', N'原矿金属量t', 4, N'002004', N'合计', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100009', N'2018', N'12', N'21', N'003', N'原矿金属量t', 4, N'003001', N'采掘一车间', CAST(-400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100010', N'2018', N'12', N'21', N'003', N'原矿金属量t', 4, N'003002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100011', N'2018', N'12', N'21', N'003', N'原矿金属量t', 4, N'003003', N'地表', CAST(600.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100012', N'2018', N'12', N'21', N'003', N'原矿金属量t', 4, N'003004', N'合计', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100013', N'2018', N'12', N'21', N'004', N'检斤量t', 4, N'004001', N'采掘一车间', CAST(22.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100014', N'2018', N'12', N'21', N'004', N'检斤量t', 4, N'004002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100015', N'2018', N'12', N'21', N'004', N'检斤量t', 4, N'004003', N'地表', CAST(300.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100016', N'2018', N'12', N'21', N'004', N'检斤量t', 4, N'004004', N'合计', CAST(22.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100017', N'2018', N'12', N'21', N'005', N'计划供矿品位%', 4, N'005001', N'采掘一车间', CAST(2.14 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100018', N'2018', N'12', N'21', N'005', N'计划供矿品位%', 4, N'005002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100019', N'2018', N'12', N'21', N'005', N'计划供矿品位%', 4, N'005003', N'地表', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100020', N'2018', N'12', N'21', N'005', N'计划供矿品位%', 4, N'005004', N'合计', CAST(2.14 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100021', N'2018', N'12', N'21', N'006', N'检斤金属量t', 4, N'006001', N'采掘一车间', CAST(0.47 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100022', N'2018', N'12', N'21', N'006', N'检斤金属量t', 4, N'006002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100023', N'2018', N'12', N'21', N'006', N'检斤金属量t', 4, N'006003', N'地表', CAST(600.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100024', N'2018', N'12', N'21', N'006', N'检斤金属量t', 4, N'006004', N'合计', CAST(0.47 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100025', N'2018', N'12', N'21', N'007', N'原矿品位%', 4, N'007001', N'采掘一车间', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100026', N'2018', N'12', N'21', N'007', N'原矿品位%', 4, N'007002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100027', N'2018', N'12', N'21', N'007', N'原矿品位%', 4, N'007003', N'地表', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100028', N'2018', N'12', N'21', N'007', N'原矿品位%', 4, N'007004', N'合计', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100029', N'2018', N'12', N'21', N'008', N'回收率', 4, N'008001', N'采掘一车间', CAST(400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100030', N'2018', N'12', N'21', N'008', N'回收率', 4, N'008002', N'采掘二车间', CAST(400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100031', N'2018', N'12', N'21', N'008', N'回收率', 4, N'008003', N'地表', CAST(400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100032', N'2018', N'12', N'21', N'008', N'回收率', 4, N'008004', N'合计', CAST(400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100033', N'2018', N'12', N'21', N'009', N'锌金属量', 4, N'009001', N'采掘一车间', CAST(4000.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100034', N'2018', N'12', N'21', N'009', N'锌金属量', 4, N'009002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100035', N'2018', N'12', N'21', N'009', N'锌金属量', 4, N'009003', N'地表', CAST(2400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100036', N'2018', N'12', N'21', N'009', N'锌金属量', 4, N'009004', N'合计', CAST(4000.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100037', N'2018', N'12', N'21', N'010', N'块矿矿量t', 1, N'010001', N'采掘一车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100038', N'2018', N'12', N'21', N'011', N'块矿锌金属量t', 1, N'011001', N'采掘一车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100039', N'2018', N'12', N'21', N'012', N'', 1, N'012005', N'', CAST(800.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100040', N'2018', N'12', N'21', N'013', N'外排矿量', 3, N'013001', N'采掘一车间', CAST(500.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100041', N'2018', N'12', N'21', N'013', N'外排矿量', 3, N'013002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100042', N'2018', N'12', N'21', N'013', N'外排矿量', 3, N'013004', N'合计', CAST(500.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100043', N'2018', N'12', N'21', N'014', N'外排金属量', 3, N'014001', N'采掘一车间', CAST(1000.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100044', N'2018', N'12', N'21', N'014', N'外排金属量', 3, N'014002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100045', N'2018', N'12', N'21', N'014', N'外排金属量', 3, N'014004', N'合计', CAST(1000.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100046', N'2018', N'12', N'21', N'015', N'原矿品位%', 3, N'015001', N'采掘一车间', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100047', N'2018', N'12', N'21', N'015', N'原矿品位%', 3, N'015002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100048', N'2018', N'12', N'21', N'015', N'原矿品位%', 3, N'015004', N'合计', CAST(200.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100049', N'2018', N'12', N'21', N'016', N'回收率', 3, N'016001', N'采掘一车间', CAST(400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100050', N'2018', N'12', N'21', N'016', N'回收率', 3, N'016002', N'采掘二车间', CAST(400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100051', N'2018', N'12', N'21', N'016', N'回收率', 3, N'016004', N'合计', CAST(400.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100052', N'2018', N'12', N'21', N'017', N'锌金属量', 3, N'017001', N'采掘一车间', CAST(4000.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100053', N'2018', N'12', N'21', N'017', N'锌金属量', 3, N'017002', N'采掘二车间', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1] ([ID], [Year], [Month], [Day], [Type1_Code], [Type1_Name], [Y1], [Type2_Code], [Type2_Name], [Val]) VALUES (N'R10002018122100054', N'2018', N'12', N'21', N'017', N'锌金属量', 3, N'017004', N'合计', CAST(4000.00 AS Numeric(18, 2)))
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'001', N'处理量t', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'001001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'001002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'001003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'001004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'002', N'原矿金属量t', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'002001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'002002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'002003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'002004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'003', N'原矿金属量t', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'003001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'003002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'003003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'003004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'004', N'检斤量t', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'004001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'004002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'004003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'004004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'005', N'计划供矿品位%', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'005001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'005002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'005003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'005004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'006', N'检斤金属量t', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'006001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'006002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'006003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'006004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'007', N'原矿品位%', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'007001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'007002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'007003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'007004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'008', N'回收率', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'008001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'008002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'008003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'008004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'009', N'锌金属量', N'4', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'009001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'009002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'009003', N'地表', N'db', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'009004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'010', N'块矿矿量t', N'1', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'010001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'011', N'块矿锌金属量t', N'1', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'011001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'012', N'', N'1', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'012005', N'', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'013', N'外排矿量', N'3', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'013001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'013002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'013004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'014', N'外排金属量', N'3', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'014001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'014002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'014004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'015', N'原矿品位%', N'3', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'015001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'015002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'015004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'016', N'回收率', N'3', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'016001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'016002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'016004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'017', N'锌金属量', N'3', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'017001', N'采掘一车间', N'1c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'017002', N'采掘二车间', N'2c', NULL, NULL)
INSERT [dbo].[T8_Report1_Config] ([FCode], [FName], [R1], [R2], [R3]) VALUES (N'017004', N'合计', NULL, NULL, NULL)
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210010001', N'2018', N'12', N'21', N'采掘一车间供矿', 3, N'TE0000000001', N'1#溜井', N'gk', CAST(2.21 AS Numeric(10, 2)), CAST(25.87 AS Numeric(10, 2)), CAST(6.80 AS Numeric(10, 2)), CAST(4.20 AS Numeric(10, 2)), CAST(90.00 AS Numeric(10, 2)), CAST(1.99 AS Numeric(10, 2)), CAST(23.28 AS Numeric(10, 2)), CAST(6.12 AS Numeric(10, 2)), CAST(3.78 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210010002', N'2018', N'12', N'21', N'采掘一车间供矿', 3, N'TE0000000002', N'2#溜井', N'gk', CAST(2.24 AS Numeric(10, 2)), CAST(26.80 AS Numeric(10, 2)), CAST(7.00 AS Numeric(10, 2)), CAST(5.00 AS Numeric(10, 2)), CAST(54.00 AS Numeric(10, 2)), CAST(1.21 AS Numeric(10, 2)), CAST(14.47 AS Numeric(10, 2)), CAST(3.78 AS Numeric(10, 2)), CAST(2.70 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210010003', N'2018', N'12', N'21', N'采掘一车间供矿', 3, N'TE0000000003', N'3#溜井', N'gk', CAST(2.24 AS Numeric(10, 2)), CAST(26.80 AS Numeric(10, 2)), CAST(7.00 AS Numeric(10, 2)), CAST(5.00 AS Numeric(10, 2)), CAST(54.00 AS Numeric(10, 2)), CAST(1.21 AS Numeric(10, 2)), CAST(14.47 AS Numeric(10, 2)), CAST(3.78 AS Numeric(10, 2)), CAST(2.70 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210011004', N'2018', N'12', N'21', N'', 0, N'HJ', N'合计', N'gk', CAST(2.23 AS Numeric(10, 2)), CAST(26.37 AS Numeric(10, 2)), CAST(6.91 AS Numeric(10, 2)), CAST(4.64 AS Numeric(10, 2)), CAST(198.00 AS Numeric(10, 2)), CAST(4.41 AS Numeric(10, 2)), CAST(52.22 AS Numeric(10, 2)), CAST(13.68 AS Numeric(10, 2)), CAST(9.18 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210013001', N'2018', N'12', N'21', N'采掘一车间检斤', 3, N'TE0000000001', N'1#溜井', N'jj', CAST(2.10 AS Numeric(10, 2)), CAST(25.90 AS Numeric(10, 2)), CAST(6.80 AS Numeric(10, 2)), CAST(4.20 AS Numeric(10, 2)), CAST(10.00 AS Numeric(10, 2)), CAST(0.21 AS Numeric(10, 2)), CAST(2.59 AS Numeric(10, 2)), CAST(0.68 AS Numeric(10, 2)), CAST(0.42 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210013002', N'2018', N'12', N'21', N'采掘一车间检斤', 3, N'TE0000000002', N'2#溜井', N'jj', CAST(2.17 AS Numeric(10, 2)), CAST(26.83 AS Numeric(10, 2)), CAST(7.00 AS Numeric(10, 2)), CAST(5.00 AS Numeric(10, 2)), CAST(6.00 AS Numeric(10, 2)), CAST(0.13 AS Numeric(10, 2)), CAST(1.61 AS Numeric(10, 2)), CAST(0.42 AS Numeric(10, 2)), CAST(0.30 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210013003', N'2018', N'12', N'21', N'采掘一车间检斤', 3, N'TE0000000003', N'3#溜井', N'jj', CAST(2.17 AS Numeric(10, 2)), CAST(26.83 AS Numeric(10, 2)), CAST(7.00 AS Numeric(10, 2)), CAST(5.00 AS Numeric(10, 2)), CAST(6.00 AS Numeric(10, 2)), CAST(0.13 AS Numeric(10, 2)), CAST(1.61 AS Numeric(10, 2)), CAST(0.42 AS Numeric(10, 2)), CAST(0.30 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210014004', N'2018', N'12', N'21', N'', 0, N'HJ', N'合计', N'jj', CAST(2.14 AS Numeric(10, 2)), CAST(26.41 AS Numeric(10, 2)), CAST(6.91 AS Numeric(10, 2)), CAST(4.64 AS Numeric(10, 2)), CAST(22.00 AS Numeric(10, 2)), CAST(0.47 AS Numeric(10, 2)), CAST(5.81 AS Numeric(10, 2)), CAST(1.52 AS Numeric(10, 2)), CAST(1.02 AS Numeric(10, 2)), N'1c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210020001', N'2018', N'12', N'21', N'采掘二车间供矿', 1, N'TE0000000005', N'5#溜井', N'gk', CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), N'2c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210021002', N'2018', N'12', N'21', N'', 0, N'HJ', N'合计', N'gk', CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), N'2c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210023001', N'2018', N'12', N'21', N'采掘二车间检斤', 1, N'TE0000000005', N'5#溜井', N'jj', CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), N'2c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210024002', N'2018', N'12', N'21', N'', 0, N'HJ', N'合计', N'jj', CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), N'2c')
INSERT [dbo].[T8_Report2] ([ID], [Year], [Month], [Day], [CJName], [Y1], [LJID], [LJName], [Type], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W], [W_Xin], [W_Tie], [W_Tong], [W_Qian], [CheJian]) VALUES (N'R2000201812210035000', N'2018', N'12', N'21', N'', 0, N'ZJ', N'总计', N'jj', CAST(2.14 AS Numeric(10, 2)), CAST(26.41 AS Numeric(10, 2)), CAST(6.91 AS Numeric(10, 2)), CAST(4.64 AS Numeric(10, 2)), CAST(22.00 AS Numeric(10, 2)), CAST(0.47 AS Numeric(10, 2)), CAST(5.81 AS Numeric(10, 2)), CAST(1.52 AS Numeric(10, 2)), CAST(1.02 AS Numeric(10, 2)), N'')
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221000010001', N'2018', N'12', N'21', N'001', N'955m', 3, N'001001', N'3212采场', CAST(2.72 AS Numeric(10, 2)), CAST(21.34 AS Numeric(10, 2)), CAST(4.00 AS Numeric(10, 2)), CAST(4.00 AS Numeric(10, 2)), CAST(18.00 AS Numeric(10, 2)), CAST(0.49 AS Numeric(10, 2)), CAST(3.84 AS Numeric(10, 2)), CAST(0.72 AS Numeric(10, 2)), CAST(0.72 AS Numeric(10, 2)), CAST(2.00 AS Numeric(10, 2)), CAST(0.05 AS Numeric(10, 2)), CAST(0.43 AS Numeric(10, 2)), CAST(0.08 AS Numeric(10, 2)), CAST(0.08 AS Numeric(10, 2)), N'1#溜井', N'1c')
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221000010002', N'2018', N'12', N'21', N'001', N'955m', 3, N'001002', N'3110采场', CAST(1.61 AS Numeric(10, 2)), CAST(27.63 AS Numeric(10, 2)), CAST(9.00 AS Numeric(10, 2)), CAST(2.00 AS Numeric(10, 2)), CAST(18.00 AS Numeric(10, 2)), CAST(0.29 AS Numeric(10, 2)), CAST(4.97 AS Numeric(10, 2)), CAST(1.62 AS Numeric(10, 2)), CAST(0.36 AS Numeric(10, 2)), CAST(2.00 AS Numeric(10, 2)), CAST(0.03 AS Numeric(10, 2)), CAST(0.55 AS Numeric(10, 2)), CAST(0.18 AS Numeric(10, 2)), CAST(0.04 AS Numeric(10, 2)), N'1#溜井', N'1c')
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221000011000', N'2018', N'12', N'21', N'001', N'955m', 3, N'HJ', N'小计', NULL, NULL, NULL, NULL, CAST(36.00 AS Numeric(10, 2)), CAST(0.78 AS Numeric(10, 2)), CAST(8.81 AS Numeric(10, 2)), CAST(2.34 AS Numeric(10, 2)), CAST(1.08 AS Numeric(10, 2)), CAST(4.00 AS Numeric(10, 2)), CAST(0.08 AS Numeric(10, 2)), CAST(0.98 AS Numeric(10, 2)), CAST(0.26 AS Numeric(10, 2)), CAST(0.12 AS Numeric(10, 2)), NULL, NULL)
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221000040001', N'2018', N'12', N'21', N'004', N'805m', 2, N'004001', N'6107矿房', CAST(2.24 AS Numeric(10, 2)), CAST(26.79 AS Numeric(10, 2)), CAST(7.00 AS Numeric(10, 2)), CAST(5.00 AS Numeric(10, 2)), CAST(54.00 AS Numeric(10, 2)), CAST(1.21 AS Numeric(10, 2)), CAST(14.47 AS Numeric(10, 2)), CAST(3.78 AS Numeric(10, 2)), CAST(2.70 AS Numeric(10, 2)), CAST(6.00 AS Numeric(10, 2)), CAST(0.13 AS Numeric(10, 2)), CAST(1.61 AS Numeric(10, 2)), CAST(0.42 AS Numeric(10, 2)), CAST(0.30 AS Numeric(10, 2)), N'1#溜井,2#溜井,3#溜井', N'1c')
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221000041000', N'2018', N'12', N'21', N'004', N'805m', 2, N'HJ', N'小计', NULL, NULL, NULL, NULL, CAST(54.00 AS Numeric(10, 2)), CAST(1.21 AS Numeric(10, 2)), CAST(14.47 AS Numeric(10, 2)), CAST(3.78 AS Numeric(10, 2)), CAST(2.70 AS Numeric(10, 2)), CAST(6.00 AS Numeric(10, 2)), CAST(0.13 AS Numeric(10, 2)), CAST(1.61 AS Numeric(10, 2)), CAST(0.42 AS Numeric(10, 2)), CAST(0.30 AS Numeric(10, 2)), NULL, NULL)
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221000050001', N'2018', N'12', N'21', N'005', N'755m', 2, N'005001', N'7113矿柱', CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), N'5#溜井', N'2c')
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221000051000', N'2018', N'12', N'21', N'005', N'755m', 2, N'HJ', N'小计', NULL, NULL, NULL, NULL, CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), NULL, NULL)
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221010000000', N'2018', N'12', N'21', NULL, NULL, 0, N'QKHJ', N'全矿合计', NULL, NULL, NULL, NULL, CAST(90.00 AS Numeric(10, 2)), CAST(1.99 AS Numeric(10, 2)), CAST(23.28 AS Numeric(10, 2)), CAST(6.12 AS Numeric(10, 2)), CAST(3.78 AS Numeric(10, 2)), CAST(10.00 AS Numeric(10, 2)), CAST(0.21 AS Numeric(10, 2)), CAST(2.59 AS Numeric(10, 2)), CAST(0.68 AS Numeric(10, 2)), CAST(0.42 AS Numeric(10, 2)), NULL, NULL)
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221020000001', N'2018', N'12', N'21', NULL, NULL, 0, N'1CHJ', N'采掘一车间', NULL, NULL, NULL, NULL, CAST(90.00 AS Numeric(10, 2)), CAST(1.99 AS Numeric(10, 2)), CAST(23.28 AS Numeric(10, 2)), CAST(6.12 AS Numeric(10, 2)), CAST(3.78 AS Numeric(10, 2)), CAST(10.00 AS Numeric(10, 2)), CAST(0.21 AS Numeric(10, 2)), CAST(2.59 AS Numeric(10, 2)), CAST(0.68 AS Numeric(10, 2)), CAST(0.42 AS Numeric(10, 2)), NULL, NULL)
INSERT [dbo].[T8_Report3] ([ID], [Year], [Month], [Day], [ZDCode], [ZDName], [Y1], [CCCode], [CCName], [PW_Xin], [PW_Tie], [PW_Tong], [PW_Qian], [W1], [W1_Xin], [W1_Tie], [W1_Tong], [W1_Qian], [W2], [W2_Xin], [W2_Tie], [W2_Tong], [W2_Qian], [EquiName], [CheJian]) VALUES (N'R300020181221020000002', N'2018', N'12', N'21', NULL, NULL, 0, N'2CHJ', N'采掘二车间', NULL, NULL, NULL, NULL, CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), NULL, NULL)
INSERT [dbo].[T8_WR] ([ID], [WorkDate], [WorkClassCode], [WorkClassName], [WorkManID], [WorkManName], [RRoleCode], [RRoleCode_Cur], [Status], [Del], [DF1], [DF2], [DF3]) VALUES (N'WR000000001120181221', N'2018-12-21', N'1', N'零点', N'UR0000000011', N'温州建设放矿工A', N'001002001001', N'001002001', N'2', N'0', NULL, NULL, NULL)
INSERT [dbo].[T8_WR] ([ID], [WorkDate], [WorkClassCode], [WorkClassName], [WorkManID], [WorkManName], [RRoleCode], [RRoleCode_Cur], [Status], [Del], [DF1], [DF2], [DF3]) VALUES (N'WR000000001220181221', N'2018-12-21', N'1', N'零点', N'UR0000000012', N'12', N'001002001001', N'001002001', N'2', N'0', NULL, NULL, NULL)
INSERT [dbo].[T8_WR_Equipment] ([ID], [WRID], [EquipmentID]) VALUES (N'WR000000001120181221001', N'WR000000001120181221', N'TE0000000001')
INSERT [dbo].[T8_WR_Equipment_D] ([ID], [WRID], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001120181221001001', N'WR000000001120181221', N'TE0000000001', N'F_3_1', N'1', N'10', N'2立方米矿车', N'45', N'吨')
INSERT [dbo].[T8_WR_Equipment_D] ([ID], [WRID], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001120181221001002', N'WR000000001120181221', N'TE0000000001', N'F_3_1', N'1', N'10', N'2立方米矿车', N'45', N'吨')
INSERT [dbo].[T8_WR_Equipment_D] ([ID], [WRID], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001120181221001003', N'WR000000001120181221', N'TE0000000001', N'F_3_1', N'2', N'10', N'2立方米矿车', N'45', N'吨')
INSERT [dbo].[T8_WR_Position] ([ID], [WRID], [PositionCode]) VALUES (N'WR000000001120181221001', N'WR000000001120181221', N'001001001')
INSERT [dbo].[T8_WR_Position] ([ID], [WRID], [PositionCode]) VALUES (N'WR000000001220181221001', N'WR000000001220181221', N'001002001')
INSERT [dbo].[T8_WR_Position] ([ID], [WRID], [PositionCode]) VALUES (N'WR000000001220181221002', N'WR000000001220181221', N'004001001')
INSERT [dbo].[T8_WR_Position_Data1] ([ID], [WRID], [PositionCode], [FKey], [Fvalue], [FUnit]) VALUES (N'WRDF000000001120181125001001', N'WRD000000001120181125001', N'001001001', N'F_1_1', N'100.00', NULL)
INSERT [dbo].[T8_WR_Position_Data2] ([ID], [WRID], [PositionCode], [EquipmentID]) VALUES (N'WR000000001120181221001001', N'WR000000001120181221', N'001001001', N'TE0000000001')
INSERT [dbo].[T8_WR_Position_Data2] ([ID], [WRID], [PositionCode], [EquipmentID]) VALUES (N'WR000000001220181221001001', N'WR000000001220181221', N'001002001', N'TE0000000001')
INSERT [dbo].[T8_WR_Position_Data2] ([ID], [WRID], [PositionCode], [EquipmentID]) VALUES (N'WR000000001220181221002001', N'WR000000001220181221', N'004001001', N'TE0000000001')
INSERT [dbo].[T8_WR_Position_Data2] ([ID], [WRID], [PositionCode], [EquipmentID]) VALUES (N'WR000000001220181221002002', N'WR000000001220181221', N'004001001', N'TE0000000002')
INSERT [dbo].[T8_WR_Position_Data2] ([ID], [WRID], [PositionCode], [EquipmentID]) VALUES (N'WR000000001220181221002003', N'WR000000001220181221', N'004001001', N'TE0000000003')
INSERT [dbo].[T8_WR_Position_Data2_D] ([ID], [WRID], [PositionCode], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001120181221001001001', N'WR000000001120181221', N'001001001', N'TE0000000001', N'F_2_1', N'1', N'10', N'1立方铲运机', N'15', N'吨')
INSERT [dbo].[T8_WR_Position_Data2_D] ([ID], [WRID], [PositionCode], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001220181221001001001', N'WR000000001220181221', N'001002001', N'TE0000000001', N'F_2_1', N'1', N'10', N'1立方铲运机', N'15', N'吨')
INSERT [dbo].[T8_WR_Position_Data2_D] ([ID], [WRID], [PositionCode], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001220181221002001001', N'WR000000001220181221', N'004001001', N'TE0000000001', N'F_2_1', N'1', N'10', N'1立方铲运机', N'15', N'吨')
INSERT [dbo].[T8_WR_Position_Data2_D] ([ID], [WRID], [PositionCode], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001220181221002001002', N'WR000000001220181221', N'004001001', N'TE0000000001', N'F_2_1', N'1', N'10', N'2立方铲运机', N'30', N'吨')
INSERT [dbo].[T8_WR_Position_Data2_D] ([ID], [WRID], [PositionCode], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001220181221002002001', N'WR000000001220181221', N'004001001', N'TE0000000002', N'F_2_1', N'1', N'10', N'1立方铲运机', N'15', N'吨')
INSERT [dbo].[T8_WR_Position_Data2_D] ([ID], [WRID], [PositionCode], [EquipmentID], [FKey], [FType], [Fvalue0], [FUnit0], [FValue1], [FUnit1]) VALUES (N'WR000000001220181221002003001', N'WR000000001220181221', N'004001001', N'TE0000000003', N'F_2_1', N'1', N'10', N'1立方铲运机', N'15', N'吨')
/****** Object:  StoredProcedure [dbo].[SP_ChuKuang_Change]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ChuKuang_Change]
	@Date varchar(10),
	@ClassCode varchar(10),
	@EquipmentID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	declare @t_position table(i int, code varchar(100))
	insert into @t_position
	select row_number() over (order by T2_Position.Code), T2_Position.Code
	from T3_Equipment_Position
		left join T2_Position on T2_Position.Code like T3_Equipment_Position.PositionCode + '%'
	where 1=1
		and T2_Position.Type = '3'
		and T2_Position.Del = '0'
		and T3_Equipment_Position.EquipmentID = @EquipmentID

	declare @t_gongkuang table(positionCode varchar(100), equipmentID varchar(100), w numeric(20, 2))
	insert into @t_gongkuang
	select 
		T8_WR_Position_Data2_D.PositionCode, @EquipmentID, sum(cast(FValue1 as numeric(18, 2)))
	from T8_WR 
		left join T8_WR_Position_Data2_D on T8_WR.ID = T8_WR_Position_Data2_D.WRID
	where 1=1
		and T8_WR.WorkDate = @Date
		and T8_WR.WorkClassCode = @ClassCode
		--and T8_WR.Status = '3'
		and T8_WR.Del = '0'
		and T8_WR_Position_Data2_D.EquipmentID = @EquipmentID
		and T8_WR_Position_Data2_D.FType = '1'
	group by T8_WR_Position_Data2_D.PositionCode
	declare @w_gongkuang numeric(20, 2)
	select
		@w_gongkuang = isnull(sum(w), 0)
	from @t_gongkuang

	declare @w_fangkuang numeric(20, 2)
	select 
		@w_fangkuang = isnull(sum(cast(FValue1 as numeric(18, 2))), 0)
	from T8_WR 
		left join T8_WR_Equipment_D on T8_WR.ID = T8_WR_Equipment_D.WRID
	where 1=1
		and T8_WR.WorkDate = @Date
		and T8_WR.WorkClassCode = @ClassCode
		--and T8_WR.Status = '3'
		and T8_WR.Del = '0'
		and T8_WR_Equipment_D.EquipmentID = @EquipmentID
		and T8_WR_Equipment_D.FType = '1'

	declare @w_jianjin numeric(20, 2)
	select 
		@w_jianjin = isnull(sum(cast(T5_WorkRecord_Detail_Field.FieldValue as numeric(18, 2))), 0)
	from T5_WorkRecord 
		left join T5_WorkRecord_Detail on T5_WorkRecord.ID = T5_WorkRecord_Detail.WorkRecordID
		left join T5_WorkRecord_Detail_Field on T5_WorkRecord_Detail.ID = T5_WorkRecord_Detail_Field.WorkRecordDetailID
	where 1=1
		and T5_WorkRecord.WorkDate = @Date
		and T5_WorkRecord.WorkClassCode = @ClassCode
		--and T8_WR.Status = '3'
		and T5_WorkRecord.Del = '0'
		and T5_WorkRecord_Detail.EquipmentID = @EquipmentID
		and T5_WorkRecord_Detail_Field.FieldKey = 'Equipment_1_1'
		and T5_WorkRecord_Detail_Field.FieldType = '1' -- 矿石

	declare @i int
	declare @c int
	declare @zymCode varchar(100)
	declare @id varchar(100)

	set @i = 1
	select @c = count(1) from @t_position

	while(@i <= @c)
	begin
		select @zymCode = code from @t_position where i = @i

		set @id = null
		select @id = ID
		from T8_ChuKuang
		where 1=1
			and WorkDate = @Date
			and WorkClassCode = @ClassCode
			and PositionCode = @zymCode
			and EquipmentID = @EquipmentID

		if(@id is null)
		begin
			set @id = 'CK' + replace(@Date, '-', '') + right(@EquipmentID, 10) + '0' + @ClassCode + '0' + @zymCode
			insert into T8_ChuKuang(ID, WorkDate, WorkClassCode, PositionCode, EquipmentID, W1, W2)
			select
				@id, @Date, @ClassCode, @zymCode, @EquipmentID, 0, 0
		end
		else
		begin
			update T8_ChuKuang
			set W1 = 0,
				w2 = 0
			where 1=1
				and ID = @id
		end

		if(@w_gongkuang = 0)
		begin
			update T8_ChuKuang
			set W1 = @w_fangkuang / @c,
				w2 = @w_jianjin / @c
			where 1=1
				and ID = @id
		end
		else
		begin
			update T8_ChuKuang
			set W1 = 
					@w_fangkuang * (
						isnull((select sum(w) from @t_gongkuang where positionCode = @zymCode), 0) / @w_gongkuang
					),
				w2 = @w_jianjin * (
						isnull((select sum(w) from @t_gongkuang where positionCode = @zymCode), 0) / @w_gongkuang
					)
			where 1=1
				and ID = @id
		end

		set @i = @i + 1
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DataInit]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DataInit]
AS
BEGIN
	SET NOCOUNT ON;

	-- 清理公告
	delete T5_MessageBoard

	-- 清理设备、井下数据
	delete T5_WorkRecord
	delete T5_WorkRecord_Detail
	delete T5_WorkRecord_Detail_Field

	-- 清理计划导入数据
	delete T6_Plan
	delete T6_Plan_B1_ZongBiao
	delete T6_Plan_B3_CaiJue
	delete T6_Plan_B4_JueJin
	delete T6_Plan_B6_CaiKuang
	delete T6_Plan_B7_ChuKuang

	-- 清理实际数据
	delete T4_MP
	delete T4_MP_Detail_1
	delete T4_MP_Detail_2

	-- 清理验收导入数据
	delete T6_Check
	delete T6_Check_B1_ZongBiao
	delete T6_Check_B3_CaiJue
	delete T6_Check_B4_JueJin
	delete T6_Check_B6_CaiKuang
	delete T6_Check_B7_ChuKuang
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Org_UpdateRemark]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Org_UpdateRemark]
	@Code varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	declare @bi int
	set @bi = len(@Code)

	declare @ei int
	set @ei = len((select top 1 Code from T2_Org where Code like @Code + '%' order by Code desc))

	while(@bi <= @ei)
	begin
		update T2_Org
		set
			Remark = (select Remark from T2_Org t where t.Code = left(T2_Org.Code, @bi - 3)) + ' - ' + STitle
		where 1=1
			and Code like @Code + '%'
			and len(Code) = @bi

		set @bi = @bi + 3
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PlanImport]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	计划导入
-- =============================================
CREATE PROCEDURE [dbo].[SP_PlanImport]
	@Year varchar(100),
	@Month varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	declare @ID varchar(100)
	select
		@ID = ID
	from T6_Plan
	where 1=1
		and YM = @Year + '/' + right('0' + @Month, 2)

	set @Month = @Year + '-' + right('0' + @Month, 2)

	if(@ID is not null)
	begin
		exec SP_PlanImport_0 @ID

		exec SP_PlanImport_1 @ID

		delete T4_MP where Month = @Month
		delete T4_MP_Detail_1 where Month = @Month
		delete T4_MP_Detail_2 where Month = @Month

		exec SP_PlanImport_3_B1 @ID, @Month
		exec SP_PlanImport_3_B4 @ID, @Month
		exec SP_PlanImport_3_B7 @ID, @Month

		exec SP_PlanImport_9 @ID, @Month
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PlanImport_0]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	计划导入
-- =============================================
CREATE PROCEDURE [dbo].[SP_PlanImport_0]
	@ID varchar(100) -- 计划ID
AS
BEGIN
	SET NOCOUNT ON;

	declare @t table(i int, id varchar(100), zd varchar(100), cc varchar(100), zym varchar(100))
	declare @i int
	declare @c int
	declare @planB4_ID varchar(100)
	declare @zd varchar(100)
	declare @cc varchar(100)
	declare @zym varchar(100)

	insert into @t
	select 
		row_number() over (order by T6_Plan_B4_JueJin.ZD, T6_Plan_B4_JueJin.ZYM),
		T6_Plan_B4_JueJin.ID, T6_Plan_B4_JueJin.ZD, null, T6_Plan_B4_JueJin.ZYM
	from T6_Plan_B4_JueJin
	where 1=1
		and PID = @ID

	set @i = 1
	select @c = count(1) from @t

	while(@i <= @c)
	begin
		select @planB4_ID = id, @zd = zd, @cc = isnull(cc, ''), @zym = zym from @t where i = @i

		if(@cc = '' and charindex('采场', @zym) > 0)
		begin
			set @cc = substring(@zym, 0, charindex('采场', @zym) + 2)
		end
		else
		begin
			set @cc = ''
		end

		update T6_Plan_B4_JueJin
		set 
			CC = @cc
		where 1=1
			and ID = @planB4_ID

		set @i = @i + 1
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PlanImport_1]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	计划导入 位置处理
-- =============================================
CREATE PROCEDURE [dbo].[SP_PlanImport_1]
	@ID varchar(100) -- 计划ID
AS
BEGIN
	SET NOCOUNT ON;

	declare @t table(i int, zd varchar(100), cc varchar(100), zym varchar(100))
	declare @i int
	declare @c int
	declare @zd varchar(100)
	declare @zdCode varchar(100)
	declare @cc varchar(100)
	declare @ccCode varchar(100)
	declare @zym varchar(100)
	declare @zymCode varchar(100)

	-- B4 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
	delete @t
	set @i = 1

	insert into @t
	select 
		row_number() over (order by T6_Plan_B4_JueJin.ZD, T6_Plan_B4_JueJin.ZYM),
		T6_Plan_B4_JueJin.ZD, null, T6_Plan_B4_JueJin.ZYM
	from T6_Plan_B4_JueJin
	where 1=1
		and PID = @ID

	select @c = count(1) from @t
	
	while(@i <= @c)
	begin
		select @zd = zd, @cc = cc, @zym = zym from @t where i = @i

		set @zdCode = null
		select @zdCode = Code
		from T2_Position
		where 1=1
			and Title = @zd

		if(@zdCode is null)
		begin
			set @zdCode = dbo.FP_Tool_CodeAddOne('', (select max(Code) from T2_Position where len(Code) = 3))

			insert into T2_Position(
				ID, Code, Type, Title, Del, Lock
			)
			select
				'TP' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_Position), 10),
				@zdCode, '1', @zd, '0', '0' 
			exec SP_Position_UpdateRemark @zdCode
		end

		set @ccCode = null
		select @ccCode = Code
		from T2_Position
		where 1=1
			and Code like @zdCode + '___'
			and Title = @cc

		if(@ccCode is null)
		begin
			set @ccCode = dbo.FP_Tool_CodeAddOne(@zdCode, (select max(Code) from T2_Position where Code like @zdCode + '___'))

			insert into T2_Position(
				ID, Code, Type, Title, Del, Lock
			)
			select
				'TP' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_Position), 10),
				@ccCode, '2', @cc, '0', '0' 
			exec SP_Position_UpdateRemark @ccCode
		end

		set @zymCode = null
		select @zymCode = Code
		from T2_Position
		where 1=1
			and Code like @ccCode + '___'
			and Title = @zym

		if(@zymCode is null)
		begin
			set @zymCode = dbo.FP_Tool_CodeAddOne(@ccCode, (select max(Code) from T2_Position where Code like @ccCode + '___'))

			insert into T2_Position(
				ID, Code, Type, Title, Del, Lock
			)
			select
				'TP' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_Position), 10),
				@zymCode, '3', @zym, '0', '0' 
			exec SP_Position_UpdateRemark @zymCode
		end

		set @i = @i + 1
	end
	-- B4 ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

	-- B7 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
	delete @t
	set @i = 1

	insert into @t
	select 
		row_number() over (order by T6_Plan_B7_ChuKuang.ZD, T6_Plan_B7_ChuKuang.CC),
		T6_Plan_B7_ChuKuang.ZD, T6_Plan_B7_ChuKuang.CC, null
	from T6_Plan_B7_ChuKuang
	where 1=1
		and PID = @ID

	select @c = count(1) from @t
	
	while(@i <= @c)
	begin
		select @zd = zd, @cc = cc, @zym = zym from @t where i = @i

		set @zdCode = null
		select @zdCode = Code
		from T2_Position
		where 1=1
			and Title = @zd

		if(@zdCode is null)
		begin
			set @zdCode = dbo.FP_Tool_CodeAddOne('', (select max(Code) from T2_Position where len(Code) = 3))

			insert into T2_Position(
				ID, Code, Type, Title, Del, Lock
			)
			select
				'TP' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_Position), 10),
				@zdCode, '1', @zd, '0', '0' 
			exec SP_Position_UpdateRemark @zdCode
		end

		if(charindex('副产', @cc) > 0)
		begin
			set @i = @i + 1
			continue
		end

		set @ccCode = null
		select @ccCode = Code
		from T2_Position
		where 1=1
			and Code like @zdCode + '___'
			and Title = @cc

		if(@ccCode is null)
		begin
			set @ccCode = dbo.FP_Tool_CodeAddOne(@zdCode, (select max(Code) from T2_Position where Code like @zdCode + '___'))

			insert into T2_Position(
				ID, Code, Type, Title, Del, Lock
			)
			select
				'TP' + dbo.FP_Tool_IDAddOne((select max(ID) from T2_Position), 10),
				@ccCode, '2', @cc, '0', '0' 
			exec SP_Position_UpdateRemark @ccCode
		end

		set @i = @i + 1
	end
	-- B7 ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PlanImport_3_B1]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	计划导入 数据汇总
-- =============================================
CREATE PROCEDURE [dbo].[SP_PlanImport_3_B1]
	@ID varchar(100),
	@Month varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	-- 放矿
	insert into T4_MP_Detail_1(Month, PositionCode, ConfigCode, Val)
	select
		@Month, '', T4_Config.Code, T6_Plan_B1_ZongBiao.BYJH
	from
		T6_Plan_B1_ZongBiao
		left join T4_Config on 1=1
			and T4_Config.Code like 'B1%'
			and T4_Config.DFKey = 'Job_3_1'
			and T6_Plan_B1_ZongBiao.ZB2 = T4_Config.Remark1
	where 1=1
		and T6_Plan_B1_ZongBiao.PID = @ID
		and T6_Plan_B1_ZongBiao.ZB1 = '各溜井出矿量'
		and T4_Config.Code is not null

	-- 提升 矿
	insert into T4_MP_Detail_1(Month, PositionCode, ConfigCode, Val)
	select
		@Month, '', T4_Config.Code, T6_Plan_B1_ZongBiao.BYJH
	from
		T6_Plan_B1_ZongBiao
		left join T4_Config on 1=1
			and T4_Config.Code like 'B1%'
			and T4_Config.DFKey = 'Job_5_1'
			and T6_Plan_B1_ZongBiao.ZB2 = T4_Config.Remark1
	where 1=1
		and T6_Plan_B1_ZongBiao.PID = @ID
		and T6_Plan_B1_ZongBiao.ZB1 = '各井筒矿石提升量'
		and T4_Config.Code is not null

	-- 提升 渣
	insert into T4_MP_Detail_1(Month, PositionCode, ConfigCode, Val)
	select
		@Month, '', T4_Config.Code, T6_Plan_B1_ZongBiao.BYJH
	from
		T6_Plan_B1_ZongBiao
		left join T4_Config on 1=1
			and T4_Config.Code like 'B1%'
			and T4_Config.DFKey = 'Job_5_2'
			and T6_Plan_B1_ZongBiao.ZB2 = T4_Config.Remark1
	where 1=1
		and T6_Plan_B1_ZongBiao.PID = @ID
		and T6_Plan_B1_ZongBiao.ZB1 = '各井筒废石提升量'
		and T4_Config.Code is not null
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PlanImport_3_B4]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	计划导入 数据汇总
-- =============================================
CREATE PROCEDURE [dbo].[SP_PlanImport_3_B4]
	@ID varchar(100),
	@Month varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	--select
	--	@Month, TP_1.Code, TP_2.Code, TP_3.Code, T6_Plan_B4_JueJin.*
	--from 
	--	T6_Plan_B4_JueJin
	--	left join T2_Position TP_1 on 1=1
	--		and TP_1.Type = '1'
	--		and T6_Plan_B4_JueJin.ZD = TP_1.Title
	--		and TP_1.Del = '0'
	--	left join T2_Position TP_2 on 1=1
	--		and TP_2.Type = '2'
	--		and TP_1.Code = left(TP_2.Code, len(TP_1.Code))
	--		and TP_2.Del = '0'
	--	left join T2_Position TP_3 on 1=1
	--		and TP_3.Type = '3'
	--		and TP_2.Code = left(TP_3.Code, len(TP_2.Code))
	--		and T6_Plan_B4_JueJin.GCMC = TP_3.Title
	--		and TP_3.Del = '0'
	--where 1=1
	--	and T6_Plan_B4_JueJin.PID = @ID

	
	insert into T4_MP(Month, PositionCode, Status, StatusChangeDate)
	select
		@Month, TP_3.Code, '', ''
	from 
		T6_Plan_B4_JueJin
		left join T2_Position TP_1 on 1=1
			and TP_1.Type = '1'
			and T6_Plan_B4_JueJin.ZD = TP_1.Title
			and TP_1.Del = '0'
		left join T2_Position TP_3 on 1=1
			and TP_3.Type = '3'
			and TP_1.Code = left(TP_3.Code, len(TP_1.Code))
			and T6_Plan_B4_JueJin.ZYM = TP_3.Title
			and TP_3.Del = '0'
	where 1=1
		and T6_Plan_B4_JueJin.PID = @ID

	insert into T4_MP_Detail_1(Month, PositionCode, ConfigCode, Val)
	select
		@Month, TP_3.Code, T4_Config.Code, T6_Plan_B4_JueJin.CD
	from
		T6_Plan_B4_JueJin
		left join T2_Position TP_1 on 1=1
			and TP_1.Type = '1'
			and T6_Plan_B4_JueJin.ZD = TP_1.Title
			and TP_1.Del = '0'
		left join T2_Position TP_3 on 1=1
			and TP_3.Type = '3'
			and TP_1.Code = left(TP_3.Code, len(TP_1.Code))
			and T6_Plan_B4_JueJin.ZYM = TP_3.Title
			and TP_3.Del = '0'
		left join T4_Config on 1=1
			and T4_Config.Code like 'B4%'
			and T4_Config.DFKey = 'Job_1_1'
	where 1=1
		and T6_Plan_B4_JueJin.PID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PlanImport_3_B7]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	计划导入 数据汇总
-- =============================================
CREATE PROCEDURE [dbo].[SP_PlanImport_3_B7]
	@ID varchar(100),
	@Month varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	--select
	--	@Month, TP_1.Code, TP_2.Code, TP_3.Code, T6_Plan_B7_ChuKuang.*
	--from 
	--	T6_Plan_B7_ChuKuang
	--	left join T2_Position TP_1 on 1=1
	--		and TP_1.Type = '1'
	--		and T6_Plan_B7_ChuKuang.ZD = TP_1.Title
	--		and TP_1.Del = '0'
	--	left join T2_Position TP_2 on 1=1
	--		and TP_2.Type = '2'
	--		and TP_1.Code = left(TP_2.Code, len(TP_1.Code))
	--		and TP_2.Del = '0'
	--	left join T2_Position TP_3 on 1=1
	--		and TP_3.Type = '3'
	--		and TP_2.Code = left(TP_3.Code, len(TP_2.Code))
	--		and T6_Plan_B7_ChuKuang.GCMC = TP_3.Title
	--		and TP_3.Del = '0'
	--where 1=1
	--	and T6_Plan_B7_ChuKuang.PID = @ID

	insert into T4_MP(Month, PositionCode, Status, StatusChangeDate)
	select
		@Month, TP_2.Code, '', ''
	from 
		T6_Plan_B7_ChuKuang
		left join T2_Position TP_1 on 1=1
			and TP_1.Type = '1'
			and T6_Plan_B7_ChuKuang.ZD = TP_1.Title
			and TP_1.Del = '0'
		left join T2_Position TP_2 on 1=1
			and TP_2.Type = '2'
			and TP_1.Code = left(TP_2.Code, len(TP_1.Code))
			and T6_Plan_B7_ChuKuang.CC = TP_2.Title
			and TP_2.Del = '0'
	where 1=1
		and T6_Plan_B7_ChuKuang.PID = @ID
		and TP_2.Code is not null

	insert into T4_MP_Detail_1(Month, PositionCode, ConfigCode, Val)
	select
		@Month, TP_2.Code, 'B7_09', T6_Plan_B7_ChuKuang.CKL
	from
		T6_Plan_B7_ChuKuang
		left join T2_Position TP_1 on 1=1
			and TP_1.Type = '1'
			and T6_Plan_B7_ChuKuang.ZD = TP_1.Title
			and TP_1.Del = '0'
		left join T2_Position TP_2 on 1=1
			and TP_2.Type = '2'
			and TP_1.Code = left(TP_2.Code, len(TP_1.Code))
			and T6_Plan_B7_ChuKuang.CC = TP_2.Title
			and TP_2.Del = '0'
	where 1=1
		and T6_Plan_B7_ChuKuang.PID = @ID
		and TP_2.Code is not null
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PlanImport_9]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	报表导入 数据格式化
-- =============================================
CREATE PROCEDURE [dbo].[SP_PlanImport_9]
	@ID varchar(100),
	@Month varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	update T4_MP
	set WorkDayCount = (
			select T6_Plan_B1_ZongBiao.BYJH
			from T6_Plan_B1_ZongBiao
			where 1=1
				and PID = @ID
				and ZB1 = '日历天数'
		)
	where 1=1
		and Month = @Month

	--update T4_MP_Detail
	--set 
	--	Val = cast((case when Val is not null and Val != '' and PATINDEX('%[^0-9|.|-|+]%', Val) = 0 then Val else '0' end) as numeric(20, 3))
	--where 1=1
	--	and Month = @Month
	--return

	declare @t table(i int, positionCode varchar(100), configCode varchar(100))
	declare @i int
	declare @c int
	declare @positionCode varchar(100)
	declare @configCode varchar(100)

	insert into @t
	select
		row_number() over (order by PositionCode), PositionCode, ConfigCode
	from T4_MP_Detail_1
	where 1=1
		and Month = @Month

	set @i = 1
	select @c = count(1) from @t

	while(@i <= @c)
	begin
		select @positionCode = positionCode, @configCode = configCode from @t where i = @i

		begin try
			update T4_MP_Detail_1
			set Val = cast(Val as numeric(20, 3))
			where 1=1
				and Month = @Month
				and PositionCode = @positionCode
				and ConfigCode = @configCode
		end try
		begin catch
			update T4_MP_Detail_1
			set Val = 0.000
			where 1=1
				and Month = @Month
				and PositionCode = @positionCode
				and ConfigCode = @configCode
		end catch

		set @i = @i + 1
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Position_UpdateRemark]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Position_UpdateRemark]
	@Code varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

    declare @bi int
	set @bi = len(@Code)

	declare @ei int
	set @ei = len((select top 1 Code from T2_Position where Code like @Code + '%' order by Code desc))

	while(@bi <= @ei)
	begin
		update T2_Position
		set
			Remark = isnull((select Remark from T2_Position t where t.Code = left(T2_Position.Code, @bi - 3)), '') + (case when @bi - 3 > 0 then ' - ' else '' end) + Title
		where 1=1
			and Code like @Code + '%'
			and len(Code) = @bi

		set @bi = @bi + 3
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Report1]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Report1]
	@Year int,
	@Month int,
	@Day int
AS
BEGIN
	SET NOCOUNT ON;

	declare @id varchar(100)
	select @id = ID from T8_Report1 where Year = @Year and Month = @Month and Day = @Day

	declare @i int
	declare @c int
	declare @fcode varchar(100)

	declare @t_config table(i int, code varchar(100), name varchar(100))
	insert into @t_config
	select row_number() over (order by FCode), FCode, FName
	from T8_Report1_Config
	where len(FCode) = 6

	if(@id is null)
	begin
		set @i = 0
		select @c = count(1) from @t_config

		while(@i <= @c)
		begin
			set @id = 'R1000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '00' + right('00' + cast(@i as varchar(100)), 3)

			insert into T8_Report1(ID, Year, Month, Day, Type1_Code, Type1_Name, Y1, Type2_Code, Type2_Name, Val)
			select
				@id, @Year, @Month, @Day, T8_Report1_Config.FCode, T8_Report1_Config.FName, T8_Report1_Config.R1, [@t_config].code, [@t_config].name, 0
			from @t_config
				left join T8_Report1_Config on left([@t_config].code, 3) = T8_Report1_Config.FCode
			where i = @i

			set @i = @i + 1
		end
	end

	declare @t_r1 table(i int, id varchar(100), type1_name varchar(100), type2_code varchar(100), cheJian varchar(100), val numeric(20,2))
	declare @type1_name varchar(100)
	declare @type2_code varchar(100)
	declare @cheJian varchar(100)

	insert into @t_r1
	select 
		row_number() over (order by T8_Report1.ID), id, T8_Report1.Type1_Name, T8_Report1.Type2_Code, T8_Report1_Config.R1, T8_Report1.Val
	from T8_Report1
		left join T8_Report1_Config on T8_Report1.Type2_Code = T8_Report1_Config.FCode
	where 1=1
		and T8_Report1.Year = @Year
		and T8_Report1.Month = @Month
		and T8_Report1.Day = @Day

	set @i = 1
	select @c = count(1) from @t_r1

	declare @t_r2 table(id varchar(100), ljID varchar(100), type varchar(100), cheJian varchar(100), pw_xin numeric(20,2), w numeric(20,2), w_xin numeric(20,2))
	insert into @t_r2
	select
		T8_Report2.ID, T8_Report2.LJID, T8_Report2.Type, T8_Report2.CheJian, T8_Report2.PW_Xin, T8_Report2.W, T8_Report2.W_Xin
	from T8_Report2
	where 1=1
		and T8_Report2.Year = @Year
		and T8_Report2.Month = @Month
		and T8_Report2.Day = @Day
	
	declare @c2			numeric(20,2) -- 处理量t 
	declare @c3			numeric(20,2) -- 处理量t 
	declare @c4			numeric(20,2) -- 处理量t 
	declare @c5			numeric(20,2) -- 处理量t --
	declare @c6			numeric(20,2) -- 原矿金属量t 
	declare @c7			numeric(20,2) -- 原矿金属量t 
	declare @c8			numeric(20,2) -- 原矿金属量t 
	declare @c9			numeric(20,2) -- 原矿金属量t --
	declare @c10		numeric(20,2) -- 原矿金属量t 
	declare @c11		numeric(20,2) -- 原矿金属量t 
	declare @c12		numeric(20,2) -- 原矿金属量t 
	declare @c13		numeric(20,2) -- 原矿金属量t 
	declare @c14		numeric(20,2) -- 检斤量t 
	declare @c15		numeric(20,2) -- 检斤量t 
	declare @c16		numeric(20,2) -- 检斤量t --
	declare @c17		numeric(20,2) -- 检斤量t
	declare @c18		numeric(20,2) -- 计划供矿品位%
	declare @c19		numeric(20,2) -- 计划供矿品位%
	declare @c20		numeric(20,2) -- 计划供矿品位%
	declare @c21		numeric(20,2) -- 计划供矿品位%
	declare @c22		numeric(20,2) -- 检斤金属量t 
	declare @c23		numeric(20,2) -- 检斤金属量t 
	declare @c24		numeric(20,2) -- 检斤金属量t 
	declare @c25		numeric(20,2) -- 检斤金属量t
	declare @c26		numeric(20,2) -- 原矿品位%
	declare @c27		numeric(20,2) -- 原矿品位%
	declare @c28		numeric(20,2) -- 原矿品位%
	declare @c29		numeric(20,2) -- 原矿品位%
	declare @c33		numeric(20,2) -- 回收率
	declare @c34		numeric(20,2) -- 回收率
	declare @c35		numeric(20,2) -- 回收率
	declare @c36		numeric(20,2) -- 回收率 --
	declare @c37		numeric(20,2) -- 锌金属量
	declare @c38		numeric(20,2) -- 锌金属量
	declare @c39		numeric(20,2) -- 锌金属量
	declare @c40		numeric(20,2) -- 锌金属量
	declare @c41		numeric(20,2) -- 块矿矿量t --
	declare @c42		numeric(20,2) -- 块矿锌金属量t --
	declare @c43		numeric(20,2) --
	declare @c44		numeric(20,2) -- 外排矿量
	declare @c45		numeric(20,2) -- 外排矿量
	declare @c46		numeric(20,2) -- 外排矿量 --
	declare @c47		numeric(20,2) -- 外排金属量
	declare @c48		numeric(20,2) -- 外排金属量
	declare @c49		numeric(20,2) -- 外排金属量
	declare @c50		numeric(20,2) -- 原矿品位%
	declare @c51		numeric(20,2) -- 原矿品位%
	declare @c52		numeric(20,2) -- 原矿品位%
	declare @c53		numeric(20,2) -- 回收率
	declare @c54		numeric(20,2) -- 回收率
	declare @c55		numeric(20,2) -- 回收率
	declare @c56		numeric(20,2) -- 锌金属量
	declare @c57		numeric(20,2) -- 锌金属量
	declare @c58		numeric(20,2) -- 锌金属量

	select @c5	= val from @t_r1 where type2_code = '001004'
	select @c9	= val from @t_r1 where type2_code = '002004'
	select @c16 = val from @t_r1 where type2_code = '004003'
	select @c36 = val from @t_r1 where type2_code = '008004'
	select @c41 = val from @t_r1 where type2_code = '010001'
	select @c42 = val from @t_r1 where type2_code = '011001'
	select @c46 = val from @t_r1 where type2_code = '013004'

	select @c14 = w			from @t_r2 where ljID = 'HJ' and type = 'jj' and cheJian = '1c'
	select @c15 = w			from @t_r2 where ljID = 'HJ' and type = 'jj' and cheJian = '2c'
	select @c17 = w			from @t_r2 where ljID = 'ZJ'
	select @c18 = pw_xin	from @t_r2 where ljID = 'HJ' and type = 'jj' and cheJian = '1c'
	select @c19 = pw_xin	from @t_r2 where ljID = 'HJ' and type = 'jj' and cheJian = '2c'
	select @c21 = pw_xin	from @t_r2 where ljID = 'ZJ'
	select @c22 = w_xin		from @t_r2 where ljID = 'HJ' and type = 'jj' and cheJian = '1c'
	select @c23 = w_xin		from @t_r2 where ljID = 'HJ' and type = 'jj' and cheJian = '2c'
	select @c25 = w_xin		from @t_r2 where ljID = 'ZJ'

	select @c4	= @c16
	select @c2	= case when @c17 = 0 then 0 else (@c5 - @c4) / @c17 * @c14 end
	select @c3	= case when @c17 = 0 then 0 else (@c5 - @c4) / @c17 * @c15 end
	select @c29 = case when @c5 = 0 then 0 else @c9 / @c5 * 100 end
	select @c28 = @c29
	select @c20 = @c28
	select @c24 = @c16 * @c20 / 100
	select @c8	= @c24
	select @c6	= case when @c25 = 0 then 0 else (@c9 - @c8) / @c25 * @c22 end
	select @c7	= case when @c25 = 0 then 0 else (@c9 - @c8) / @c25 * @c23 end
	select @c10 = @c6 + @c42
	select @c11 = @c7
	select @c12 = @c8
	select @c13 = @c10 + @c11 + @c12
	select @c26 = case when @c2 = 0 then 0 else @c10 / @c2 * 100 end
	select @c27 = case when @c3 = 0 then 0 else @c7 / @c3 * 100 end
	select @c33 = @c36
	select @c34 = @c33
	select @c35 = @c36
	select @c37 = @c10 * @c33 / 100
	select @c38 = @c11 * @c34 / 100
	select @c39 = @c12 * @c35 / 100
	select @c40 = @c13 * @c36 / 100
	select @c43 = @c40 - @c42 * @c36 / 100
	select @c44 = case when @c17 = 0 then 0 else @c46 / @c17 * @c14 end
	select @c45 = case when @c17 = 0 then 0 else @c46 / @c17 * @c15 end
	select @c50 = @c26
	select @c51 = @c27
	select @c52 = @c29
	select @c47 = @c44 * @c50 / 100
	select @c48 = @c45 * @c51 / 100
	select @c49 = @c46 * @c52 / 100
	select @c53 = @c33
	select @c54 = @c34
	select @c55 = @c36
	select @c56 = @c47 * @c53 / 100
	select @c57 = @c48 * @c54 / 100
	select @c58 = @c49 * @c55 / 100

	while(@i <= @c)
	begin
		select @id = id, @type1_name = type1_name, @type2_code = type2_code, @cheJian = cheJian from @t_r1 where i = @i

		if(@type1_name = '处理量t')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c2 when @cheJian = '2c' then @c3 when @cheJian = 'db' then @c4 else Val end
			where ID = @id
		end
		if(@type1_name = '原矿金属量t' and @type2_code like '002%')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c6 when @cheJian = '2c' then @c7 when @cheJian = 'db' then @c8 else Val end
			where ID = @id
		end
		if(@type1_name = '原矿金属量t' and @type2_code like '003%')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c10 when @cheJian = '2c' then @c11 when @cheJian = 'db' then @c12 when @cheJian is null then @c13 else Val end
			where ID = @id
		end
		if(@type1_name = '检斤量t')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c14 when @cheJian = '2c' then @c15 when @cheJian is null then @c17 else Val end
			where ID = @id
		end
		if(@type1_name = '计划供矿品位%')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c18 when @cheJian = '2c' then @c19 when @cheJian = 'db' then @c20 when @cheJian is null then @c21 else Val end
			where ID = @id
		end
		if(@type1_name = '检斤金属量t')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c22 when @cheJian = '2c' then @c23 when @cheJian = 'db' then @c24 when @cheJian is null then @c25 else Val end
			where ID = @id
		end
		if(@type1_name = '原矿品位%')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c26 when @cheJian = '2c' then @c27 when @cheJian = 'db' then @c28 when @cheJian is null then @c29 else Val end
			where ID = @id
		end
		if(@type1_name = '回收率')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c33 when @cheJian = '2c' then @c34 when @cheJian = 'db' then @c35 when @cheJian is null then @c36 else Val end
			where ID = @id
		end
		if(@type1_name = '锌金属量')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c37 when @cheJian = '2c' then @c38 when @cheJian = 'db' then @c39 when @cheJian is null then @c40 else Val end
			where ID = @id
		end
		if(@type1_name = '块矿矿量t')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c41 else Val end
			where ID = @id
		end
		if(@type1_name = '块矿锌金属量t')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c42 else Val end
			where ID = @id
		end
		if(@type2_code like '012%')
		begin
			update T8_Report1
			set Val = case when @cheJian is null then @c43 else Val end
			where ID = @id
		end
		if(@type1_name = '外排矿量')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c44 when @cheJian = '2c' then @c45 else Val end
			where ID = @id
		end
		if(@type1_name = '外排金属量')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c47 when @cheJian = '2c' then @c48 when @cheJian is null then @c49 else Val end
			where ID = @id
		end
		if(@type1_name = '原矿品位%')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c50 when @cheJian = '2c' then @c51 when @cheJian is null then @c52 else Val end
			where ID = @id
		end
		if(@type1_name = '回收率')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c53 when @cheJian = '2c' then @c54 when @cheJian is null then @c55 else Val end
			where ID = @id
		end
		if(@type1_name = '锌金属量')
		begin
			update T8_Report1
			set Val = case when @cheJian = '1c' then @c56 when @cheJian = '2c' then @c57 when @cheJian is null then @c58 else Val end
			where ID = @id
		end

		set @i = @i + 1
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Report2]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Report2]
	@Year int,
	@Month int,
	@Day int
AS
BEGIN
	SET NOCOUNT ON;

	declare @id varchar(100)
	select @id = ID from T8_Report2 where Year = @Year and Month = @Month and Day = @Day

	declare @i int
	set @i = 1
	declare @c int
	set @c = 2

	declare @t_equi table(i int, id varchar(100), name varchar(100))
	declare @ii int
	declare @cc int
	declare @equiID varchar(100)
	declare @equiName varchar(100)

	if(@id is null)
	begin
		declare @t_cc_chejian table(i int, positionCode varchar(100), chejian varchar(100))
		insert into @t_cc_chejian
		select 
			row_number() over (order by T2_Position.Code), T2_Position.Code,
			case when T2_Org.Title = '采掘一车间' then '1c' when T2_Org.Title = '采掘二车间' then '2c' else '' end
		from T2_Org
			left join T2_Position_Org on T2_Position_Org.OrgCode like T2_Org.Code + '%'
			left join T2_Position on T2_Position.Code like T2_Position_Org.PositionCode + '%' and T2_Position.Type = '2' and T2_Position.Del = '0'
		where 1=1
			and T2_Org.Title in ('采掘一车间', '采掘二车间')
			and T2_Org.Del = '0'
			and T2_Position.ID is not null

		declare @t_chejian_equi table(id varchar(100), ccCode varchar(100))

		while(@i <= @c)
		begin
			delete @t_chejian_equi
			insert into @t_chejian_equi
			select T3_Equipment.ID, T3_Equipment_Position.PositionCode
			from T3_Equipment
				left join @t_cc_chejian on [@t_cc_chejian].chejian = cast(@i as varchar(100)) + 'c'
				left join T3_Equipment_Position on T3_Equipment.ID = T3_Equipment_Position.EquipmentID
			where 1=1
				and T3_Equipment.Del = '0'
				and T3_Equipment_Position.PositionCode = [@t_cc_chejian].positionCode

			delete @t_equi
			insert into @t_equi
			select
				row_number() over (order by T3_Equipment.ID), T3_Equipment.ID, T3_Equipment.Title
			from T3_Equipment
			where 1=1
				and T3_Equipment.Del = '0'
				and T3_Equipment.ID in (
						select id
						from @t_chejian_equi
					)

			set @ii = 1
			select @cc = count(1) from @t_equi

			while(@ii <= @cc)
			begin
				select @equiID = id, @equiName = name from @t_equi where i = @ii

				set @id = 'R2000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '00' + cast(@i as varchar(100)) + '0' + right('00' + cast(@ii as varchar(100)), 3)

				insert into T8_Report2(
					ID, Year, Month, Day, CJName, Y1, LJID, LJName, Type,
					PW_Xin, PW_Tie, PW_Tong, PW_Qian,
					W, W_Xin, W_Tie, W_Tong, W_Qian,
					CheJian
				)
				select
					@id, @Year, @Month, @Day, case when @i = 1 then '采掘一车间供矿' when @i = 2 then '采掘二车间供矿' else '' end, @cc, @equiID, @equiName, 'gk',
					0, 0, 0, 0,
					0, 0, 0, 0, 0,
					cast(@i as varchar(100)) + 'c'

				set @id = 'R2000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '00' + cast(@i as varchar(100)) + '3' + right('00' + cast(@ii as varchar(100)), 3)

				insert into T8_Report2(
					ID, Year, Month, Day, CJName, Y1, LJID, LJName, Type,
					PW_Xin, PW_Tie, PW_Tong, PW_Qian,
					W, W_Xin, W_Tie, W_Tong, W_Qian,
					CheJian
				)
				select
					@id, @Year, @Month, @Day, case when @i = 1 then '采掘一车间检斤' when @i = 2 then '采掘二车间检斤' else '' end, @cc, @equiID, @equiName, 'jj',
					0, 0, 0, 0,
					0, 0, 0, 0, 0,
					cast(@i as varchar(100)) + 'c'

				set @ii = @ii + 1
			end

			if(@cc > 0)
			begin
				set @id = 'R2000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '00' + cast(@i as varchar(100)) + '1' + right('00' + cast(@ii as varchar(100)), 3)

				insert into T8_Report2(
					ID, Year, Month, Day, CJName, Y1, LJID, LJName, Type,
					PW_Xin, PW_Tie, PW_Tong, PW_Qian,
					W, W_Xin, W_Tie, W_Tong, W_Qian,
					CheJian
				)
				select
					@id, @Year, @Month, @Day, '', 0, 'HJ', '合计', 'gk',
					0, 0, 0, 0,
					0, 0, 0, 0, 0,
					cast(@i as varchar(100)) + 'c'

				set @id = 'R2000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '00' + cast(@i as varchar(100)) + '4' + right('00' + cast(@ii as varchar(100)), 3)

				insert into T8_Report2(
					ID, Year, Month, Day, CJName, Y1, LJID, LJName, Type,
					PW_Xin, PW_Tie, PW_Tong, PW_Qian,
					W, W_Xin, W_Tie, W_Tong, W_Qian,
					CheJian
				)
				select
					@id, @Year, @Month, @Day, '', 0, 'HJ', '合计', 'jj',
					0, 0, 0, 0,
					0, 0, 0, 0, 0,
					cast(@i as varchar(100)) + 'c'
			end

			set @i = @i + 1
		end

		set @id = 'R2000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '003' + '5' + '000'

		insert into T8_Report2(
			ID, Year, Month, Day, CJName, Y1, LJID, LJName, Type,
			PW_Xin, PW_Tie, PW_Tong, PW_Qian,
			W, W_Xin, W_Tie, W_Tong, W_Qian,
			CheJian
		)
		select
			@id, @Year, @Month, @Day, '', 0, 'ZJ', '总计', 'jj',
			0, 0, 0, 0,
			0, 0, 0, 0, 0,
			''
	end

	declare @cjName varchar(100)
	declare @ljID varchar(100)
	declare @ljName varchar(100)
	declare @type varchar(100)
	declare @cheJian varchar(100)

	declare @t_r2 table(i int, id varchar(100), cjName varchar(100), ljID varchar(100), ljName varchar(100), type varchar(100), cheJian varchar(100))
	insert into @t_r2
	select row_number() over (order by ID), ID, CJName, LJID, LJName, Type, CheJian
	from T8_Report2
	where 1=1
		and Year = @Year
		and Month = @Month
		and Day = @Day

	set @i = 1
	select @c = count(1) from @t_r2

	declare @w numeric(20, 2)
	declare @w_Xin numeric(20, 2)
	declare @w_Tie numeric(20, 2)
	declare @w_Tong numeric(20, 2)
	declare @w_Qian numeric(20, 2)

	while(@i <= @c)
	begin
		select @id = id, @cjName = cjName, @ljID = ljID, @ljName = ljName, @type = type, @cheJian = cheJian from @t_r2 where i = @i

		select
			@w		= 0,
			@w_Xin	= 0,
			@w_Tie	= 0,
			@w_Tong = 0,
			@w_Qian = 0

		if(@ljID not in ('HJ', 'ZJ') and @type = 'gk')
		begin
			select
				@w		= sum(T8_Report3.W1		),
				@w_Xin	= sum(T8_Report3.W1_Xin	),
				@w_Tie	= sum(T8_Report3.W1_Tie	),
				@w_Tong = sum(T8_Report3.W1_Tong),
				@w_Qian = sum(T8_Report3.W1_Qian)
			from T8_Report3
			where 1=1
				and T8_Report3.Year = @Year
				and T8_Report3.Month = @Month
				and T8_Report3.Day = @Day
				and T8_Report3.EquiName like '%' + @ljName + '%'
		end
		if(@ljID not in ('HJ', 'ZJ') and @type = 'jj')
		begin
			select
				@w		= sum(T8_Report3.W2		),
				@w_Xin	= sum(T8_Report3.W2_Xin	),
				@w_Tie	= sum(T8_Report3.W2_Tie	),
				@w_Tong = sum(T8_Report3.W2_Tong),
				@w_Qian = sum(T8_Report3.W2_Qian)
			from T8_Report3
			where 1=1
				and T8_Report3.Year = @Year
				and T8_Report3.Month = @Month
				and T8_Report3.Day = @Day
				and T8_Report3.EquiName like '%' + @ljName + '%'
		end
		if(@ljID = 'HJ' and @type = 'gk')
		begin
			select
				@w		= sum(T8_Report2.W		),
				@w_Xin	= sum(T8_Report2.W_Xin	),
				@w_Tie	= sum(T8_Report2.W_Tie	),
				@w_Tong = sum(T8_Report2.W_Tong	),
				@w_Qian = sum(T8_Report2.W_Qian	)
			from T8_Report2
			where 1=1
				and T8_Report2.Year = @Year
				and T8_Report2.Month = @Month
				and T8_Report2.Day = @Day
				and T8_Report2.Type = 'gk'
				and T8_Report2.CheJian = @cheJian
				and T8_Report2.LJID not in ('HJ', 'ZJ')
		end
		if(@ljID = 'HJ' and @type = 'jj')
		begin
			select
				@w		= sum(T8_Report2.W		),
				@w_Xin	= sum(T8_Report2.W_Xin	),
				@w_Tie	= sum(T8_Report2.W_Tie	),
				@w_Tong = sum(T8_Report2.W_Tong	),
				@w_Qian = sum(T8_Report2.W_Qian	)
			from T8_Report2
			where 1=1
				and T8_Report2.Year = @Year
				and T8_Report2.Month = @Month
				and T8_Report2.Day = @Day
				and T8_Report2.Type = 'jj'
				and T8_Report2.CheJian = @cheJian
				and T8_Report2.LJID not in ('HJ', 'ZJ')
		end
		if(@ljID = 'ZJ')
		begin
			select
				@w		= sum(T8_Report2.W		),
				@w_Xin	= sum(T8_Report2.W_Xin	),
				@w_Tie	= sum(T8_Report2.W_Tie	),
				@w_Tong = sum(T8_Report2.W_Tong	),
				@w_Qian = sum(T8_Report2.W_Qian	)
			from T8_Report2
			where 1=1
				and T8_Report2.Year = @Year
				and T8_Report2.Month = @Month
				and T8_Report2.Day = @Day
				and T8_Report2.Type = 'jj'
				and T8_Report2.LJID not in ('HJ', 'ZJ')
		end

		update T8_Report2
		set
			PW_Xin =	case when @w = 0 then 0 else @w_Xin		* 100.00 / @w end,
			PW_Tie =	case when @w = 0 then 0 else @w_Tie		* 100.00 / @w end,
			PW_Tong =	case when @w = 0 then 0 else @w_Tong	* 100.00 / @w end,
			PW_Qian =	case when @w = 0 then 0 else @w_Qian	* 100.00 / @w end,
			W =			@w,
			W_Xin =		@w_Xin,
			W_Tie =		@w_Tie,
			W_Tong =	@w_Tong,
			W_Qian =	@w_Qian
		where 1=1
			and T8_Report2.ID = @id

		set @i = @i + 1
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Report3]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Report3]
	@Year int,
	@Month int,
	@Day int
AS
BEGIN
	SET NOCOUNT ON;

	declare @planId varchar(100)
	declare @pw_xin numeric(20,2)
	declare @pw_tie numeric(20,2)
	declare @pw_tong numeric(20,2)
	declare @pw_qian numeric(20,2)
	select @planId = ID
	from T6_Plan
	where 1=1
		and T6_Plan.YM = cast(@Year as varchar(100)) + '/' + right('0' + cast(@Month as varchar(100)), 2)

	declare @equipmentNames varchar(1000)

	declare @id varchar(100)
	select @id = ID from T8_Report3 where Year = @Year and Month = @Month and Day = @Day

	if(@id is null)
	begin
		declare @t_zd table(i int, positionCode varchar(100), name varchar(100))
		declare @t_cc table(i int, positionCode varchar(100), name varchar(100))
		declare @t_cc_chejian table(i int, positionCode varchar(100), chejian varchar(100))
		declare @i int
		declare @c int
		declare @zdCode varchar(100)
		declare @zdName varchar(100)
		declare @ii int
		declare @cc int
		declare @ccCode varchar(100)
		declare @ccName varchar(100)

		insert into @t_cc_chejian
		select 
			row_number() over (order by T2_Position.Code), T2_Position.Code,
			case when T2_Org.Title = '采掘一车间' then '1c' when T2_Org.Title = '采掘二车间' then '2c' else '' end
		from T2_Org
			left join T2_Position_Org on T2_Position_Org.OrgCode like T2_Org.Code + '%'
			left join T2_Position on T2_Position.Code like T2_Position_Org.PositionCode + '%' and T2_Position.Type = '2' and T2_Position.Del = '0'
		where 1=1
			and T2_Org.Title in ('采掘一车间', '采掘二车间')
			and T2_Org.Del = '0'

		insert into @t_zd
		select row_number() over (order by T2_Position.Code), T2_Position.Code, T2_Position.Title
		from T2_Position
		where 1=1
			and T2_Position.Type = '1'
			and T2_Position.Del = '0'

		set @i = 1
		select @c = count(1) from @t_zd

		while(@i <= @c)
		begin
			select @zdCode = positionCode, @zdName = name from @t_zd where i = @i

			delete @t_cc
			insert into @t_cc
			select row_number() over (order by T2_Position.Code), T2_Position.Code, T2_Position.Title
			from T2_Position
			where 1=1
				and T2_Position.Type = '2'
				and T2_Position.Code like @zdCode + '%'
				and T2_Position.Del = '0'

			set @ii = 1
			select @cc = count(1) from @t_cc

			while(@ii <= @cc)
			begin
				set @id = 'R3000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '00' + right('00' + cast(@i as varchar(100)), 3) + '0' + right('00' + cast(@ii as varchar(100)), 3)
				select @ccCode = positionCode, @ccName = name from @t_cc where i = @ii

				select
					@pw_xin =  0,
					@pw_tie =  0,
					@pw_tong = 0,
					@pw_qian = 0
				select
					@pw_xin = DZPW_X,
					@pw_tie = DZPW_T,
					@pw_tong = DZPW_C * 100.00,
					@pw_qian = DZPW_L * 100.00
				from T6_Plan_B7_ChuKuang
				where 1=1
					and PID = @planId
					and ZD = @zdName
					and CC = @ccName

				set @equipmentNames = ''
				select 
					@equipmentNames = @equipmentNames + (case when @equipmentNames = '' then '' else ',' end) + T3_Equipment.Title
				from T3_Equipment_Position
					left join T3_Equipment on T3_Equipment_Position.EquipmentID = T3_Equipment.ID
				where 1=1
					and T3_Equipment_Position.PositionCode = @ccCode
					and T3_Equipment.Del = '0'

				insert into T8_Report3(
					ID, Year, Month, Day, ZDCode, ZDName, Y1, CCCode, CCName,
					PW_Xin, PW_Tie, PW_Tong, PW_Qian,
					W1, W1_Xin, W1_Tie, W1_Tong, W1_Qian,
					W2, W2_Xin, W2_Tie, W2_Tong, W2_Qian,
					EquiName, CheJian
				)
				select
					@id, @Year, @Month, @Day, @zdCode, @zdName, @cc + 1, @ccCode, @ccName,
					@pw_xin, @pw_tie, @pw_tong, @pw_qian,
					0, 0, 0, 0, 0,
					0, 0, 0, 0, 0,
					@equipmentNames, [@t_cc_chejian].chejian
				from @t_cc
					left join @t_cc_chejian on [@t_cc].positionCode = [@t_cc_chejian].positionCode
				where [@t_cc].i = @ii

				set @ii = @ii + 1
			end

			if(@cc > 0)
			begin
				insert into T8_Report3(
					ID, Year, Month, Day, ZDCode, ZDName, Y1, CCCode, CCName,
					W1, W1_Xin, W1_Tie, W1_Tong, W1_Qian,
					W2, W2_Xin, W2_Tie, W2_Tong, W2_Qian
				)
				select
					'R3000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '00' + right('00' + cast(@i as varchar(100)), 3) + '1000',
					@Year, @Month, @Day, @zdCode, @zdName, @cc + 1, 'HJ', '小计',
					0, 0, 0, 0, 0,
					0, 0, 0, 0, 0
			end

			set @i = @i + 1
		end

		insert into T8_Report3(
			ID, Year, Month, Day, ZDCode, ZDName, Y1, CCCode, CCName,
			W1, W1_Xin, W1_Tie, W1_Tong, W1_Qian,
			W2, W2_Xin, W2_Tie, W2_Tong, W2_Qian
		)
		select
			'R3000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '010000000',
			@Year, @Month, @Day, null, null, 0, 'QKHJ', '全矿合计',
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0

		insert into T8_Report3(
			ID, Year, Month, Day, ZDCode, ZDName, Y1, CCCode, CCName,
			W1, W1_Xin, W1_Tie, W1_Tong, W1_Qian,
			W2, W2_Xin, W2_Tie, W2_Tong, W2_Qian
		)
		select
			'R3000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '020000001',
			@Year, @Month, @Day, null, null, 0, '1CHJ', '采掘一车间',
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0

		insert into T8_Report3(
			ID, Year, Month, Day, ZDCode, ZDName, Y1, CCCode, CCName,
			W1, W1_Xin, W1_Tie, W1_Tong, W1_Qian,
			W2, W2_Xin, W2_Tie, W2_Tong, W2_Qian
		)
		select
			'R3000' + cast(@Year as varchar(100)) + right('0' + cast(@Month as varchar(100)), 2) + right('0' + cast(@Day as varchar(100)), 2) + '020000002',
			@Year, @Month, @Day, null, null, 0, '2CHJ', '采掘二车间',
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0
	end

	declare @t_r3 table(i int, id varchar(100), zdCode varchar(100), ccCode varchar(100))
	insert into @t_r3
	select row_number() over (order by CCCode), ID, ZDCode, CCCode
	from T8_Report3
	where 1=1
		and Year = @Year
		and Month = @Month
		and Day = @Day

	set @i = 1
	select @c = count(1) from @t_r3

	declare @w1_sum numeric(18,2)
	declare @w2_sum numeric(18,2)

	while(@i <= @c)
	begin
		select @id = id, @zdCode = zdCode, @ccCode = ccCode from @t_r3 where i = @i

		if(@ccCode not in ('HJ', 'QKHJ', '1CHJ', '2CHJ'))
		begin
			select @w1_sum = isnull(sum(W1), 0), @w2_sum = isnull(sum(W2), 0)
			from T8_ChuKuang
			where 1=1
				and T8_ChuKuang.WorkDate = cast(@Year as varchar(100)) + '-' + right('0' + cast(@Month as varchar(100)), 2) + '-' + right('0' + cast(@Day as varchar(100)), 2)
				and T8_ChuKuang.PositionCode like @ccCode + '%'

			update T8_Report3
			set 
				W1 =		@w1_sum,
				W1_Xin =	@w1_sum * PW_Xin	/ 100.00,
				W1_Tie =	@w1_sum * PW_Tie	/ 100.00,
				W1_Tong =	@w1_sum * PW_Tong	/ 100.00,
				W1_Qian =	@w1_sum * PW_Qian	/ 100.00,
				W2 =		@w2_sum,
				W2_Xin =	@w2_sum * PW_Xin	/ 100.00,
				W2_Tie =	@w2_sum * PW_Tie	/ 100.00,
				W2_Tong =	@w2_sum * PW_Tong	/ 100.00,
				W2_Qian =	@w2_sum * PW_Qian	/ 100.00
			where 1=1
				and ID = @id
		end
		if(@ccCode = 'HJ')
		begin
			update T8_Report3
			set
				T8_Report3.W1			= t.W1,
				T8_Report3.W1_Xin		= t.W1_Xin,
				T8_Report3.W1_Tie		= t.W1_Tie,
				T8_Report3.W1_Tong		= t.W1_Tong,
				T8_Report3.W1_Qian		= t.W1_Qian,
				T8_Report3.W2			= t.W2,
				T8_Report3.W2_Xin		= t.W2_Xin,
				T8_Report3.W2_Tie		= t.W2_Tie,
				T8_Report3.W2_Tong		= t.W2_Tong,
				T8_Report3.W2_Qian		= t.W2_Qian
			from (
					select
						sum(W1)			W1,
						sum(W1_Xin)		W1_Xin,
						sum(W1_Tie)		W1_Tie,
						sum(W1_Tong)	W1_Tong,
						sum(W1_Qian)	W1_Qian,
						sum(W2)			W2,
						sum(W2_Xin)		W2_Xin,
						sum(W2_Tie)		W2_Tie,
						sum(W2_Tong)	W2_Tong,
						sum(W2_Qian)	W2_Qian
					from T8_Report3
					where 1=1
						and Year = @Year
						and Month = @Month
						and Day = @Day
						and ZDCode = @zdCode
						and CCCode not in ('HJ', 'QKHJ', '1CHJ', '2CHJ')
				) t
			where 1=1
				and T8_Report3.ID = @id
		end
		if(@ccCode = 'QKHJ')
		begin
			update T8_Report3
			set
				T8_Report3.W1			= t.W1,
				T8_Report3.W1_Xin		= t.W1_Xin,
				T8_Report3.W1_Tie		= t.W1_Tie,
				T8_Report3.W1_Tong		= t.W1_Tong,
				T8_Report3.W1_Qian		= t.W1_Qian,
				T8_Report3.W2			= t.W2,
				T8_Report3.W2_Xin		= t.W2_Xin,
				T8_Report3.W2_Tie		= t.W2_Tie,
				T8_Report3.W2_Tong		= t.W2_Tong,
				T8_Report3.W2_Qian		= t.W2_Qian
			from (
					select
						sum(W1)			W1,
						sum(W1_Xin)		W1_Xin,
						sum(W1_Tie)		W1_Tie,
						sum(W1_Tong)	W1_Tong,
						sum(W1_Qian)	W1_Qian,
						sum(W2)			W2,
						sum(W2_Xin)		W2_Xin,
						sum(W2_Tie)		W2_Tie,
						sum(W2_Tong)	W2_Tong,
						sum(W2_Qian)	W2_Qian
					from T8_Report3
					where 1=1
						and Year = @Year
						and Month = @Month
						and Day = @Day
						and CCCode not in ('HJ', 'QKHJ', '1CHJ', '2CHJ')
				) t
			where 1=1
				and T8_Report3.ID = @id
		end
		if(@ccCode = '1CHJ')
		begin
			update T8_Report3
			set
				T8_Report3.W1			= t.W1,
				T8_Report3.W1_Xin		= t.W1_Xin,
				T8_Report3.W1_Tie		= t.W1_Tie,
				T8_Report3.W1_Tong		= t.W1_Tong,
				T8_Report3.W1_Qian		= t.W1_Qian,
				T8_Report3.W2			= t.W2,
				T8_Report3.W2_Xin		= t.W2_Xin,
				T8_Report3.W2_Tie		= t.W2_Tie,
				T8_Report3.W2_Tong		= t.W2_Tong,
				T8_Report3.W2_Qian		= t.W2_Qian
			from (
					select
						sum(W1)			W1,
						sum(W1_Xin)		W1_Xin,
						sum(W1_Tie)		W1_Tie,
						sum(W1_Tong)	W1_Tong,
						sum(W1_Qian)	W1_Qian,
						sum(W2)			W2,
						sum(W2_Xin)		W2_Xin,
						sum(W2_Tie)		W2_Tie,
						sum(W2_Tong)	W2_Tong,
						sum(W2_Qian)	W2_Qian
					from T8_Report3
					where 1=1
						and Year = @Year
						and Month = @Month
						and Day = @Day
						and CCCode not in ('HJ', 'QKHJ', '1CHJ', '2CHJ')
						and CheJian = '1c'
				) t
			where 1=1
				and T8_Report3.ID = @id
		end
		if(@ccCode = '2CHJ')
		begin
			update T8_Report3
			set
				T8_Report3.W1			= t.W1,
				T8_Report3.W1_Xin		= t.W1_Xin,
				T8_Report3.W1_Tie		= t.W1_Tie,
				T8_Report3.W1_Tong		= t.W1_Tong,
				T8_Report3.W1_Qian		= t.W1_Qian,
				T8_Report3.W2			= t.W2,
				T8_Report3.W2_Xin		= t.W2_Xin,
				T8_Report3.W2_Tie		= t.W2_Tie,
				T8_Report3.W2_Tong		= t.W2_Tong,
				T8_Report3.W2_Qian		= t.W2_Qian
			from (
					select
						sum(W1)			W1,
						sum(W1_Xin)		W1_Xin,
						sum(W1_Tie)		W1_Tie,
						sum(W1_Tong)	W1_Tong,
						sum(W1_Qian)	W1_Qian,
						sum(W2)			W2,
						sum(W2_Xin)		W2_Xin,
						sum(W2_Tie)		W2_Tie,
						sum(W2_Tong)	W2_Tong,
						sum(W2_Qian)	W2_Qian
					from T8_Report3
					where 1=1
						and Year = @Year
						and Month = @Month
						and Day = @Day
						and CCCode not in ('HJ', 'QKHJ', '1CHJ', '2CHJ')
						and CheJian = '2c'
				) t
			where 1=1
				and T8_Report3.ID = @id
		end

		set @i = @i + 1
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RRole_Change]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	数据审核
-- =============================================
CREATE PROCEDURE [dbo].[SP_RRole_Change]
	@ID varchar(100),
	@UsreID varchar(100),
	@RResult varchar(100), -- 审核结果 0 通过/1 不通过
	@RRemark varchar(1000), -- 审核意见
	@Ret varchar(100) output
	-- 0 错误
	-- 1 成功
AS
BEGIN
	SET NOCOUNT ON;

	set @Ret = '1'

	declare @RRoleCode_Cur varchar(100)
	declare @RRoleType_Cur varchar(100)
	declare @RRoleCode_Cur_New varchar(100)
	declare @RRoleType_Cur_New varchar(100)

	select 
		@RRoleCode_Cur = T5_WorkRecord.RRoleCode_Cur,
		@RRoleType_Cur = T2_RRole.Type
	from T5_WorkRecord
		left join T2_RRole on T5_WorkRecord.RRoleCode_Cur = T2_RRole.Code
	where 1=1
		and T5_WorkRecord.ID = @ID

	-- 不通过
	if(@RResult = '1')
	begin
		update T5_WorkRecord
		set 
			RRoleCode_Cur = RRoleCode,
			Status = '1'
		where 1=1
			and ID = @ID

		insert into T2_RRole_OperLog(
			ID,
			WorkRecordID, RRoleCode, UserID, Result, RDate,
			Remark
		)
		select 
			'RRO' + replace(@ID, 'WR', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T2_RRole_OperLog where ID like 'RRO' + replace(@ID, 'WR', '') + '%'), 5),
			@ID, @RRoleCode_Cur, @UsreID, @RResult, getdate(),
			@RRemark

		return
	end

	if(@RRoleCode_Cur is null or @RRoleCode_Cur = '')
	begin
		-- 错误
		set @Ret = '0'
		return
	end

	if(len(@RRoleCode_Cur) < 6)
	begin
		-- 错误
		set @Ret = '0'
		return
	end

	set @RRoleCode_Cur_New = left(@RRoleCode_Cur, len(@RRoleCode_Cur) - 3)
	if(len(@RRoleCode_Cur_New) < 3)
	begin
		-- 错误
		set @Ret = '0'
		return
	end

	while(1=1)
	begin
		select
			@RRoleCode_Cur_New = Code,
			@RRoleType_Cur_New = Type
		from T2_RRole
		where 1=1
			and Code = @RRoleCode_Cur_New

		-- 找不到数据类型 说明数据不存在
		if(@RRoleType_Cur_New is null)
		begin
			-- 错误
			set @Ret = '0'
			return
		end

		if(@RRoleType_Cur_New = '3')
		begin
			-- 错误
			set @Ret = '0'
			return
		end

		if(@RRoleType_Cur_New = '2')
		begin
			break
		end

		if(@RRoleType_Cur_New = '1')
		begin
			if(len(@RRoleCode_Cur_New) = 3)
			begin
				break
			end
			else
			begin
				set @RRoleCode_Cur_New = left(@RRoleCode_Cur_New, len(@RRoleCode_Cur_New) - 3)
			end
		end
	end

	declare @Status varchar(100)
	set @Status = '1'

	-- 1、草稿 ==> 审核中
    if(@RRoleType_Cur = '3' and @RRoleType_Cur_New = '2')
	begin
		set @Status = '2'
	end

	-- 2、草稿 ==> 结束
    if(@RRoleType_Cur = '3' and @RRoleType_Cur_New = '1')
	begin
		set @Status = '3'
	end

	-- 3、审核中 ==> 审核中
    if(@RRoleType_Cur = '2' and @RRoleType_Cur_New = '2')
	begin
		set @Status = '2'
	end

	-- 3、审核中 ==> 结束
    if(@RRoleType_Cur = '2' and @RRoleType_Cur_New = '1')
	begin
		set @Status = '3'
	end

	update T5_WorkRecord
	set 
		RRoleCode_Cur = @RRoleCode_Cur_New,
		Status = @Status
	where ID = @ID

	insert into T2_RRole_OperLog(
		ID,
		WorkRecordID, RRoleCode, UserID, Result, RDate,
		Remark
	)
	select 
		'RRO' + replace(@ID, 'WR', '') + dbo.FP_Tool_IDAddOne_2((select max(ID) from T2_RRole_OperLog where ID like 'RRO' + replace(@ID, 'WR', '') + '%'), 5),
		@ID, @RRoleCode_Cur, @UsreID, @RResult, getdate(),
		@RRemark
END
GO
/****** Object:  StoredProcedure [dbo].[SP_WorkRecord_UpdateDF]    Script Date: 2019/1/7 18:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	工作记录录入时 汇总显示字段
-- =============================================
CREATE PROCEDURE [dbo].[SP_WorkRecord_UpdateDF]
	@ID varchar(100), 
	@DetailID varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	declare @str varchar(8000)
	declare @str1 varchar(8000)
	declare @RecordType varchar(100)
	declare @t_df table(dfKey varchar(100))
	declare @UserID varchar(100)

	select
		@RecordType = RecordType,
		@UserID = WorkManID
	from T5_WorkRecord
	where 1=1
		and ID = @ID

	-- 设备
	if(@RecordType = 'E')
	begin
		delete @t_df
		insert into @t_df
		select
			T3_Dynamic_Field.FieldKey
		from T5_WorkRecord_Detail
			left join T3_Equipment on 1=1
				and T5_WorkRecord_Detail.EquipmentID = T3_Equipment.ID
			left join T3_Dynamic_Field on 1=1
				and T3_Dynamic_Field.Type1 = 'Equipment'
				and T3_Dynamic_Field.Type2 = T3_Equipment.Type
		where 1=1
			and T5_WorkRecord_Detail.ID = @DetailID
			and T3_Dynamic_Field.FieldKey in ('Equipment_1_1', 'Equipment_2_1')

		update T5_WorkRecord_Detail_Field
		set
			FieldUnit = '吨',
			FieldValue0 = FieldValue,
			FieldUnit0 = '吨'
		where 1=1
			and WorkRecordDetailID = @DetailID
			and FieldKey in ('Equipment_1_1', 'Equipment_2_1')
	end

	-- 井下数据
	if(@RecordType = 'M')
	begin
		delete @t_df
		insert into @t_df
		select
			T3_Dynamic_Field.FieldKey
		from T5_WorkRecord_Detail
			left join T3_Dynamic_Field on 1=1
				and T3_Dynamic_Field.Type1 = 'Job'
				and T3_Dynamic_Field.Type2 = (select JobCode from T1_User where ID = @UserID)
		where 1=1
			and T5_WorkRecord_Detail.ID = @DetailID
			and T3_Dynamic_Field.FieldKey in ('Job_1_1', 'Job_3_1', 'Job_4_1')

		update T5_WorkRecord_Detail_Field
		set
			FieldUnit = (case when T3_Dynamic_FieldUnit.Title is null then T1_DataDirc.DircTitle else T3_Dynamic_FieldUnit.Title end),
			FieldValue0 = cast((case when T3_Dynamic_FieldUnit.Title is null then cast(FieldValue as numeric(20, 3)) else cast(FieldValue as numeric(20, 3)) * T3_Dynamic_FieldUnit.Unit_0_Rate end) as numeric(20, 3)),
			FieldUnit0 = T1_DataDirc.DircTitle
		from
			T3_Dynamic_Field 
			left join T3_Dynamic_FieldUnit on T3_Dynamic_Field.FieldKey = T3_Dynamic_FieldUnit.DFKey
			left join T1_DataDirc on T1_DataDirc.Type = 'FieldUnit' and T1_DataDirc.DircKey = T3_Dynamic_Field.FieldUnit
		where 1=1
			and T5_WorkRecord_Detail_Field.WorkRecordDetailID = @DetailID
			and T5_WorkRecord_Detail_Field.FieldKey = T3_Dynamic_Field.FieldKey
			and T5_WorkRecord_Detail_Field.FieldKey in ('Job_3_1', 'Job_4_1')
			and T5_WorkRecord_Detail_Field.FieldUnit = T3_Dynamic_FieldUnit.Type

		update T5_WorkRecord_Detail_Field
		set
			FieldUnit = T1_DataDirc.DircTitle,
			FieldValue0 = cast(FieldValue as numeric(20, 3)),
			FieldUnit0 = T1_DataDirc.DircTitle
		from
			T3_Dynamic_Field
			left join T1_DataDirc on T1_DataDirc.Type = 'FieldUnit' and T1_DataDirc.DircKey = T3_Dynamic_Field.FieldUnit
		where 1=1
			and T5_WorkRecord_Detail_Field.WorkRecordDetailID = @DetailID
			and T5_WorkRecord_Detail_Field.FieldKey = T3_Dynamic_Field.FieldKey
			and T5_WorkRecord_Detail_Field.FieldKey in ('Job_1_1', 'Job_1_2')
	end

	update T5_WorkRecord_Detail_Field
	set 
		FieldType = null
	where 1=1
		and T5_WorkRecord_Detail_Field.WorkRecordDetailID = @DetailID
		and T5_WorkRecord_Detail_Field.FieldKey not in ('Equipment_1_1', 'Equipment_2_1', 'Job_3_1', 'Job_4_1')

	update T5_WorkRecord_Detail_Field
	set FieldUnit = null
	where 1=1
		and T5_WorkRecord_Detail_Field.WorkRecordDetailID = @DetailID
		and T5_WorkRecord_Detail_Field.FieldKey not in ('Equipment_1_1', 'Equipment_2_1', 'Job_1_1', 'Job_1_2', 'Job_3_1', 'Job_4_1')

	set @str = ''
	select
		@str = @str + (case when @str = '' then '' else ', ' end) 
			+ T3_Dynamic_Field.FieldName + ': ' 
			+ '#' + T3_Dynamic_Field.FieldKey + '# '
	from @t_df t_df
		left join T3_Dynamic_Field on t_df.dfKey = T3_Dynamic_Field.FieldKey

	set @str1 = @str
	select
		@str1 = replace(@str1, '#' + T5_WorkRecord_Detail_Field.FieldKey + '#', sum(cast(T5_WorkRecord_Detail_Field.FieldValue0 as numeric(20, 3))))
			+ T5_WorkRecord_Detail_Field.FieldUnit0
	from @t_df t_df
		left join T5_WorkRecord_Detail_Field on 1=1
			and T5_WorkRecord_Detail_Field.WorkRecordDetailID = @DetailID 
			and t_df.dfKey = T5_WorkRecord_Detail_Field.FieldKey
	group by T5_WorkRecord_Detail_Field.FieldKey, T5_WorkRecord_Detail_Field.FieldUnit0

	update T5_WorkRecord_Detail
	set
		DF1 = @str1
	where 1=1
		and ID = @DetailID

	-- 设备
	if(@RecordType = 'E')
	begin
		set @str1 = @str
		select
			@str1 = replace(@str1, '#' + T3_Dynamic_Field.FieldKey + '#', sum(cast(T5_WorkRecord_Detail_Field.FieldValue0 as numeric(20, 3))))
				+ T5_WorkRecord_Detail_Field.FieldUnit0
		from 
			T5_WorkRecord_Detail
			left join T5_WorkRecord_Detail_Field on 1=1
				and T5_WorkRecord_Detail_Field.WorkRecordDetailID = T5_WorkRecord_Detail.ID
			left join T3_Dynamic_Field on 1=1
				and T5_WorkRecord_Detail_Field.FieldKey = T3_Dynamic_Field.FieldKey
		where 1=1
			and T5_WorkRecord_Detail.WorkRecordID = @ID
			and T3_Dynamic_Field.FieldKey in ('Equipment_1_1', 'Equipment_2_1')
		group by T3_Dynamic_Field.FieldKey, T5_WorkRecord_Detail_Field.FieldUnit0
	end

	-- 井下数据
	if(@RecordType = 'M')
	begin
		set @str1 = @str
		select
			@str1 = replace(@str1, '#' + T3_Dynamic_Field.FieldKey + '#', sum(cast(T5_WorkRecord_Detail_Field.FieldValue0 as numeric(20, 3))))
				+ T5_WorkRecord_Detail_Field.FieldUnit0
		from 
			T5_WorkRecord_Detail
			left join T5_WorkRecord_Detail_Field on 1=1
				and T5_WorkRecord_Detail_Field.WorkRecordDetailID = T5_WorkRecord_Detail.ID
			left join T3_Dynamic_Field on 1=1
				and T5_WorkRecord_Detail_Field.FieldKey = T3_Dynamic_Field.FieldKey
		where 1=1
			and T5_WorkRecord_Detail.WorkRecordID = @ID
			and T3_Dynamic_Field.FieldKey in ('Job_1_1', 'Job_3_1', 'Job_4_1')
		group by T3_Dynamic_Field.FieldKey, T5_WorkRecord_Detail_Field.FieldUnit0
	end

	update T5_WorkRecord
	set
		DF1 = @str1
	where 1=1
		and ID = @ID
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_DataDirc', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_DataDirc', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_DataDirc', @level2type=N'COLUMN',@level2name=N'DircKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_DataDirc', @level2type=N'COLUMN',@level2name=N'DircTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_DataDirc', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_Page', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 1 PC/2 手持' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_Page', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_Page', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_Page', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_Page', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'OrgCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'PRoleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审批层级编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'RRoleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据权限类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'DRoleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工种编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'JobCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工唯一标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'UserKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Admin', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Admin', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Admin', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Admin', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'OrgCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审批层级编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'RRoleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据权限类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'DRoleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工种编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'JobCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工唯一标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'UserKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T1_User_Excel', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_DRole', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_DRole', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 0 不限/1 自身及下属/2 自身' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_DRole', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_DRole', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Org', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Org', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 1 矿/2 企/3 自定义' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Org', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Org', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Org', @level2type=N'COLUMN',@level2name=N'STitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Org', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Org', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 1 中段/2 采场/3 作业面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position_Org', @level2type=N'COLUMN',@level2name=N'PositionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_Position_Org', @level2type=N'COLUMN',@level2name=N'OrgCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_PRole', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_PRole', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_PRole', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_PRole', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面权限表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_PRole_Detail', @level2type=N'COLUMN',@level2name=N'PRoleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_PRole_Detail', @level2type=N'COLUMN',@level2name=N'PageCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审批权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'层级编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 1 接收/2 审批/3 提交' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审批日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole_OperLog', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole_OperLog', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审批结果 0 通过/1 退回' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole_OperLog', @level2type=N'COLUMN',@level2name=N'Result'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结果描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T2_RRole_OperLog', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动态指标表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Dynamic_Field', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'gongzhong 工种/shebei 设备' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Dynamic_Field', @level2type=N'COLUMN',@level2name=N'Type1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工种编码 / 设备ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Dynamic_Field', @level2type=N'COLUMN',@level2name=N'Type2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Dynamic_Field', @level2type=N'COLUMN',@level2name=N'I'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Dynamic_Field', @level2type=N'COLUMN',@level2name=N'FieldKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Dynamic_Field', @level2type=N'COLUMN',@level2name=N'FieldName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Dynamic_Field', @level2type=N'COLUMN',@level2name=N'FieldUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Equipment', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Equipment', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Equipment', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工种表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Job', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工种编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Job', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Job', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T3_Job', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录类型 E 设备/M 人员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'RecordType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作日期 2018-09-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'WorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'WorkClassCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'WorkClassName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'WorkManID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'WorkManName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前审核层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'RRoleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前审核层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'RRoleCode_Cur'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据状态 0 未填写/1 草稿/2 审核中/3 正式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'DF1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'DF2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord', @level2type=N'COLUMN',@level2name=N'DF3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作记录详细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作记录表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'WorkRecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'EquipmentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'PositionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作时长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'WorkHour'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'DF1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'DF2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail', @level2type=N'COLUMN',@level2name=N'DF3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作记录详细字段表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail_Field', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作记录详细表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail_Field', @level2type=N'COLUMN',@level2name=N'WorkRecordDetailID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail_Field', @level2type=N'COLUMN',@level2name=N'FieldKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail_Field', @level2type=N'COLUMN',@level2name=N'FieldValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T5_WorkRecord_Detail_Field', @level2type=N'COLUMN',@level2name=N'FieldValue0'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收年月' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check', @level2type=N'COLUMN',@level2name=N'YM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收文件名_服务器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check', @level2type=N'COLUMN',@level2name=N'FileNameS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check', @level2type=N'COLUMN',@level2name=N'UploadTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'CID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'ZB1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'ZB2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'DW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本月验收' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'BYYS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'CID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'ZB1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'ZB2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'DW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年度验收' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'NDYS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止上月预计完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'YJWC1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止上月完成率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'WCL1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本月验收' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'BYYS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止本月预计完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'YJWC2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止本月完成率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'WCL2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'CID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ZD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采场' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'CC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ZYM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程类型（性质）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'GCLX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'台效（m/班）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'TX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'台班（个）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'TB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格（m x m）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'GG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'断面积（m2）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'DMJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长度（m）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'CD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'体积（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'TJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'掘进量（t）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'JJL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折合标米（m）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ZHBM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副产（t）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'FC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'施工时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'SGSJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机台' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B4_JueJin', @level2type=N'COLUMN',@level2name=N'JT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'CID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'ZD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采场' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'CC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采矿类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'CKLX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_锌（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铁（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_T'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铜（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_C'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铅（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_L'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采矿量（t）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'CKL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'填充总量（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'TCZL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尾砂量（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'WSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'胶结量（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'JJL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'KSSJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'JSSJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'BZ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'ZD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采场' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗矿量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'XHKL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品位_锌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品位_铁' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_T'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铜（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_C'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铅（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_L'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贫化率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'PHL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'损失率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'SSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出矿品位_锌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CKPW_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出矿品位_铁' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CKPW_T'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出矿量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Check_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CKL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划年月' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan', @level2type=N'COLUMN',@level2name=N'YM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划文件名_服务器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan', @level2type=N'COLUMN',@level2name=N'FileNameS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan', @level2type=N'COLUMN',@level2name=N'UploadTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'ZB1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'ZB2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'DW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本月计划' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B1_ZongBiao', @level2type=N'COLUMN',@level2name=N'BYJH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'ZB1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'ZB2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'DW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年度计划' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'NDJH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止上月预计完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'YJWC1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止上月完成率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'WCL1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本月计划' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'BYJH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止本月预计完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'YJWC2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止本月完成率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B3_CaiJue', @level2type=N'COLUMN',@level2name=N'WCL2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ZD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采场' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'CC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ZYM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程类型（性质）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'GCLX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'台效（m/班）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'TX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'台班（个）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'TB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格（m x m）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'GG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'断面积（m2）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'DMJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长度（m）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'CD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'体积（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'TJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'掘进量（t）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'JJL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折合标米（m）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'ZHBM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副产（t）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'FC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'施工时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'SGSJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机台' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin', @level2type=N'COLUMN',@level2name=N'JT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划-掘进计划' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B4_JueJin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'ZD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采场' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'CC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采矿类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'CKLX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_锌（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铁（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_T'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铜（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_C'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铅（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'DZPW_L'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采矿量（t）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'CKL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'填充总量（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'TCZL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尾砂量（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'WSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'胶结量（m3）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'JJL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'KSSJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'JSSJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B6_CaiKuang', @level2type=N'COLUMN',@level2name=N'BZ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'ZD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采场' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗矿量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'XHKL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品位_锌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品位_铁' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_T'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铜（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_C'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地质品味_铅（%）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'DZPW_L'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贫化率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'PHL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'损失率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'SSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出矿品位_锌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CKPW_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出矿品位_铁' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CKPW_T'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出矿量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T6_Plan_B7_ChuKuang', @level2type=N'COLUMN',@level2name=N'CKL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样本编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采样时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'STime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采样地点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采样人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'Sampler'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样本备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样本分析结果' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'Result'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分析结果输入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'RTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样本分析人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample', @level2type=N'COLUMN',@level2name=N'Analyst'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采样表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T7_Sample'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_ChuKuang', @level2type=N'COLUMN',@level2name=N'WorkClassCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作日期 2018-09-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'WorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'WorkClassCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'WorkClassName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'WorkManID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'WorkManName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前审核层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'RRoleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前审核层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'RRoleCode_Cur'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据状态 0 未填写/1 草稿/2 审核中/3 正式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废 0 正常/1 已作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'DF1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'DF2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR', @level2type=N'COLUMN',@level2name=N'DF3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作记录表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR_Position', @level2type=N'COLUMN',@level2name=N'WRID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T8_WR_Position', @level2type=N'COLUMN',@level2name=N'PositionCode'
GO
USE [master]
GO
ALTER DATABASE [HLAQSC] SET  READ_WRITE 
GO
