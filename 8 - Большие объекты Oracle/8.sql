/*
BLOB - �������� ����� �-�               ���� � �� (����)
CLOB - ���������� �-�                   ���� � �� (����)
BILE - ���� ����� ��, ���. ����� �-�    ��� �� (����)

LOB ���� �� �������� (���� �� �� ����) + �������� (�������� ����������)
*/

-- ���� ��� ����� ��� WORD (PDF)
create directory bfile_dir as 'D:\pict\';


-- ��� � ���� FOTO BLOB, DOC BFILE
create table BigFiles (
    username varchar2(50) not null,
    foto blob not null,
    doc bfile
    );


-- ��� ����� � ���� � �������
insert into BigFiles
    values ('Julia',
            utl_raw.cast_to_raw('D:\pict\mysh.png'),
            BFILENAME( 'bfile_dir', 'db.docx'));


select * from BigFiles;





