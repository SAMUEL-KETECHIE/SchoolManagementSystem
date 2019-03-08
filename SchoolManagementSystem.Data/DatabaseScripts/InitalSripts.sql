--Table Structures--
create database preschool;

CREATE table "Role"(
  "RoleId" serial primary key,
  "RoleName" text not null default 'Admin'::text
);

CREATE table "Users"(
  "UserId" serial primary key,
  "Username" text not null,
  "Password" text not null,
  "IsActive" boolean not null default true,
  "RoleId" integer not null references "Role"("RoleId")
);

CREATE table "Classes"(
  "ClassId" serial primary key ,
  "ClassName" text not null default 'Admin'::text
);



create table "Students"(
  "StudentId" bigserial primary key,
  "StudentName" text not null,
  "StudentAddress" text,
  "StudentNo" text,
  "DateOfBirth" timestamp without time zone not null,
  "Age" integer not null,
  "Gender" text not null default 'Other'::text,
  "ParentName" text,
  "DateEnrolled" timestamp without time zone,
  "IsActive" boolean not null default true,
  "Image" text,
  "ClassId" integer not null references "Classes"("ClassId")
);


CREATE table "Subjects"(
  "SubjectId" serial primary key ,
  "SubjectName" text not null default 'Admin'::text
);



CREATE table "Teachers"(
  "TeacherId" serial primary key ,
  "TeacherName" text not null ,
  "TeacherNo" text not null,
  "TeacherAddress" text not null ,
  "Image" text,
  "IsActive" boolean not null default true,
  "SubjectId" integer not null references "Subjects"("SubjectId")
);

--DB Users--

CREATE USER root with password 'root';
alter user root with superuser ;

--------############Store Procedures############-------------

--Get All Students--
create or replace function getallstudents()
returns setof "Students"
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY SELECT * from "Students";
END;
$$;

--Get All Teachers--
create or replace function getallteachers()
returns setof "Teachers"
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY SELECT * from "Teachers";
END;
$$;

--Get All Classes
create or replace function getallclasses()
returns setof "Classes"
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY SELECT * from "Classes";
END;
$$;


create or replace function getallsubjects()
returns setof "Subjects"
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY SELECT * from "Subjects";
END;
$$;

create or replace function getallusers()
returns setof "Users"
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY SELECT * from "Users";
END;
$$;

create or replace function getallroles()
returns setof "Role"
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY SELECT * from "Role";
END;
$$;


--Insert Procedures--
create or replace function addstudents(studentname text,address text,studentno text ,dateofbirth timestamp without time zone,age integer,gender text,parentname text,enrolleddate timestamp without time zone,image text,classid integer)
returns setof "Students"
LANGUAGE plpgsql
AS $$
    declare existId integer;
BEGIN
  insert into "Students"("StudentName", "StudentAddress", "StudentNo", "DateOfBirth", "Age","Gender", "ParentName", "DateEnrolled", "Image", "ClassId")
  VALUES(studentname,address,studentno,dateofbirth,age,gender,parentname,enrolleddate,image,classid) returning "StudentId" into existId;

  return query select * from "Students" where "StudentId"=existId;
END;
$$;

--SELECT * FROM addstudents('Samuel Wendolin','Legon,Accra','WEN19BIT2019','1991-01-01',28,'Male','Akufo Addo','2019-02-27','\images\wen.png',1);


create or replace function addteachers(teachername text,teacherno text ,address text,image text,subjectid integer)
returns setof "Teachers"
LANGUAGE plpgsql
AS $$
    declare newId integer;
BEGIN
  insert into "Teachers"("TeacherName", "TeacherNo", "TeacherAddress", "Image", "IsActive", "SubjectId")
  VALUES (teachername,teacherno,address,image,true,subjectid) returning "TeacherId" into newId;

  return query select * from "Teachers" where "Teachers"."TeacherId"=newId;
END;
$$;

--select * from addteachers('Alfred Barnor','TCHOH123','Westland-Accra','\images\tea1.png',1);

create or replace function addsubjects(subjectname text)
     returns setof "Subjects"
     LANGUAGE plpgsql
     AS $$
         declare newId integer;
     BEGIN
       insert into "Subjects"("SubjectName") values (subjectname) returning "SubjectId" into newId;

       return query select * from "Subjects" where "Subjects"."SubjectId" =newId;
     END;
     $$;

--select * from addsubjects('English Language');

create or replace function addclasses(classname text)
     returns setof "Classes"
     LANGUAGE plpgsql
     AS $$
         declare newId integer;
     BEGIN
       insert into "Classes"("ClassName") values (classname) returning "ClassId" into newId;

       return query select * from "Classes" where "Classes"."ClassId" =newId;
     END;
     $$;

--select * from addclasses('Primary 2');

create or replace function adduser(username text,password text,roleid integer)
     returns setof "Users"
     LANGUAGE plpgsql
     AS $$
         declare newId integer;
     BEGIN
       insert into "Users"("Username","Password", "RoleId") values (username,password,roleid) returning "UserId" into newId;

       return query select * from "Users" where "UserId" =newId;
     END;
     $$;

--select * from adduser('Admin','admin',1);

create or replace function addrole(rolename text)
     returns setof "Role"
     LANGUAGE plpgsql
     AS $$
         declare newId integer;
     BEGIN
       insert into "Role"("RoleName") values (rolename) returning "RoleId" into newId;

       return query select * from "Role" where "RoleId" =newId;
     END;
     $$;

--select * from addrole('Administrators');


create or replace function getstudentbyinfo(info text) returns SETOF "Students"
	language plpgsql
as $$
  declare str text;
BEGIN
    str=concat('%',info,'%');
  RETURN QUERY SELECT * from "Students" where "StudentName" like str or "StudentNo" like str or "StudentAddress" like str or "ParentName" like str or "Gender" like str;
END;
$$;

select * from getstudentbyinfo('Sam');

create or replace function getteacherbyinfo(info text) returns SETOF "Teachers"
	language plpgsql
as $$
   declare str text;
BEGIN
    str=concat('%',info,'%');
  RETURN QUERY SELECT * from "Teachers" where "TeacherName" like str or "TeacherNo" like str or "TeacherAddress" like str;
END;
$$;

--select * from getteacherbyinfo('Alfred');

create or replace function getsubjectbyinfo(info text) returns SETOF "Subjects"
	language plpgsql
as $$
    declare str text;
BEGIN
  str=concat('%',info,'%');
  RETURN QUERY SELECT * from "Subjects" where "SubjectName" like str;
END;
$$;

--select * from getsubjectbyinfo('Mathema');

create or replace function getclassbyinfo(info text) returns SETOF "Classes"
  language plpgsql
as
$$
  declare str text;
BEGIN
    str=concat('%',info,'%');
  RETURN QUERY SELECT * from "Classes" where "ClassName" like str;
END;
$$;

--select * from getclassbyinfo('Prim');