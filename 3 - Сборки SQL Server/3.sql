--����� - ��������� ���������� �����
-- CLR (Common Lang Runtime) - ��������. ������ �����
-- SQL CLR: ���������, �-�, ����, ��������



--drop procedure GetCount;
--drop assembly MyAssembly;
--drop type Otdel;


use Sklad;

exec sp_configure 'clr_enabled', 1			--��� sql clr
exec sp_configure 'clr_strict_security', 0
reconfigure

alter assembly MyAssembly
	from 'D:\3\��\3\3\bin\Debug\_3.dll'
	with permission_set = safe;
go


-------------------------------- � � � � � � � � � -----------------------------------

create procedure GetCount (@dateStart datetime, @dateEnd datetime)
as external name MyAssembly.StoredProcedures.GetCount


declare @ref int
exec @ref = GetCount '01-01-1998', '01-02-2019'
print @ref


------------------------------- � � �    C L R ---------------------------------------

create type Otdel
external name MyAssembly.SqlUserDefinedType1;
go


declare @s Otdel
set @s = '������� ��������� 50 2'
select @s.ToString();
go


------------------------------ P I V O T ---------------------------------------------

DECLARE @cols  AS NVARCHAR(MAX)='';
DECLARE @query AS NVARCHAR(MAX)='';
SELECT @cols = @cols + QUOTENAME(order_year) + ',' FROM (select distinct order_year from orders ) as tmp
select @cols = substring(@cols, 0, len(@cols)) --trim "," at end
print @cols


set @query = 'select * from orders pivot (sum(summ) for order_year in (' + @cols + ')) piv'
execute(@query)
