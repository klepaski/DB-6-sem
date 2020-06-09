--Склад - структура складского места
-- CLR (Common Lang Runtime) - общеязык. исполн среда
-- SQL CLR: процедуры, ф-и, типы, триггеры



--drop procedure GetCount;
--drop assembly MyAssembly;
--drop type Otdel;


use Sklad;

exec sp_configure 'clr_enabled', 1			--вкл sql clr
exec sp_configure 'clr_strict_security', 0
reconfigure

alter assembly MyAssembly
	from 'D:\3\БД\3\3\bin\Debug\_3.dll'
	with permission_set = safe;
go


-------------------------------- П Р О Ц Е Д У Р А -----------------------------------

create procedure GetCount (@dateStart datetime, @dateEnd datetime)
as external name MyAssembly.StoredProcedures.GetCount


declare @ref int
exec @ref = GetCount '01-01-1998', '01-02-2019'
print @ref


------------------------------- Т И П    C L R ---------------------------------------

create type Otdel
external name MyAssembly.SqlUserDefinedType1;
go


declare @s Otdel
set @s = 'Теплица Чистякова 50 2'
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
