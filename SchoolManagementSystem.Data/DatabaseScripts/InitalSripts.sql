--Table Structures--
create database preschool;
use preschool;

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

--Store Procedures

