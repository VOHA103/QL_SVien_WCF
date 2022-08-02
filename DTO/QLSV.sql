create database QL_SinhVien
go
use QLSinhVien
go

create table SINH_VIEN
(
MaSV varchar(5) primary key,
TenSV nvarchar(50) not null,
GioiTinh nvarchar(11),
NamNhapHoc int,
)
go

insert into SINH_VIEN values('SV1',N'Ha',N'Nữ',2019);
go

select*from SINH_VIEN
go
delete  from SINH_VIEN where MaSV='SV1'