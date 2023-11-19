drop database if exists proje;

create database proje;

use proje;

-- Tables

	create table timetable (
		time_id int not null auto_increment,
		
		timetable_day int not null,
		timetable_hour int not null,

		primary key (time_id),
		unique key (timetable_day, timetable_hour)
	);

	-- ------------------------------------------------

	create table students (
		student_id int not null,
		
		tr_id char(11) not null,
		first_name varchar(16) not null,
		last_name varchar(16) not null,
		birth_date date not null,
		
		primary key(student_id),
		unique key(tr_id)
	);
	
		create table active_students (
			active_student_id int not null,
			
			foreign key (active_student_id) references students(student_id)
		);
	
		create table graduated_students (
			graduated_student_id int not null,
			
			foreign key (graduated_student_id) references students(student_id)
		);
	
	-- ------------------------------------------------
		
	create table parents (
		student_id int not null,
		
		tr_id char(11) not null,
		first_name varchar(16) not null,
		last_name varchar(16) not null,
		phone_number char(11),
		email varchar(64),
		
		foreign key(student_id) references students(student_id),
		primary key(student_id)
	);
	
	-- ------------------------------------------------
	
	create table expenses (
		expense_id int not null auto_increment,
		
		expense_name varchar(32) not null,
		amount int not null,
		is_fixed boolean not null,
		expense_date date not null,
		
		primary key(expense_id)
	);

	--	create table fixed_expense (
	--		fixed_expense_id int not null,
	--		
	--		foreign key (fixed_expense_id) references expenses(expense_id),
	--		primary key (fixed_expense_id)
	--	);
	--
	--	create table onetime_expense (
	--		onetime_expense_id int not null,
	--		
	--		foreign key (onetime_expense_id) references expenses(expense_id),
	--		primary key (onetime_expense_id)
	--	);
	
	-- ------------------------------------------------
	
	create table courses (
		course_id char(6) not null,
		
		course_name varchar(32) not null,
		course_minimum_demand_to_open int not null,
		
		primary key (course_id),
		unique key(course_name)
	);
	
	create table course_times(
		course_id char(6) not null,
		course_time_id int not null,
		
		foreign key (course_time_id) references timetable (time_id),
		foreign key (course_id) references courses (course_id),
		unique key (course_id, course_time_id)
	);
	
	create table materials (
		material_id int not null auto_increment,
		
		material_amount int not null,
		material_name varchar(32) not null,
		
		primary key (material_id)
	);
	
	-- ------------------------------------------------
	
	create table employees (
		employee_id int not null,
		
		tr_id char(11) not null,
		first_name varchar(25) not null,
		last_name varchar(25) not null,

		salary int not null,
		
		primary key (employee_id),
		unique key (tr_id)
	);
	
		create table janitors (
			janitor_id int not null,
			
			foreign key (janitor_id) references employees(employee_id)
		);
		
		create table instructors (
			instructor_id int not null,
			
			foreign key (instructor_id) references employees(employee_id)
		);
		
		create table administrative_staffs (
			administrative_staff_id int not null,
			
			foreign key (administrative_staff_id) references employees(employee_id)
		);
	
	-- ------------------------------------------------
	
	create table instructor_to_courses (
		instructor_id int not null,
		course_id char(6) not null,
		
		foreign key (instructor_id) references instructors(instructor_id),
		foreign key (course_id) references courses(course_id),
		unique key (instructor_id, course_id)
	);
	
	create table instructor_to_available_hours ( -- Assumed all instructors will have their working hours entered via this list including both part and fulltime instructors
		instructor_id int not null,
		available_time_id int not null,
		
		foreign key (available_time_id) references timetable (time_id),
		foreign key (instructor_id) references instructors (instructor_id),
		unique key (instructor_id, available_time_id)
	);

	-- ------------------------------------------------

	create table material_to_courses(
		course_id char(6) not null,
		material_id int not null,
		
		foreign key (course_id) references courses (course_id),
		foreign key (material_id) references materials (material_id),
		primary key (course_id, material_id)
	);
	
	-- ------------------------------------------------
	
	create table active_student_to_available_hours (
		active_student_id int not null,
		available_time_id int not null,
		
		foreign key (available_time_id) references timetable (time_id),
		foreign key (active_student_id) references active_students (active_student_id),
		key(active_student_id, available_time_id)
	);

	-- ------------------------------------------------

	create table student_demand_to_course (
		student_id int not null,
		course_id char(6) not null,
		
		foreign key (student_id) references students (student_id),
		foreign key (course_id) references courses (course_id),
		unique key (student_id, course_id)
	);

-- Triggers

	-- ------------------------------------------------
	
	create trigger delete_student_parents before delete on students for each row -- Delete parents of a deleted student
		delete from parents where parents.student_id=old.student_id;

	create trigger delete_active_student after delete on active_students for each row -- Delete student as well when a active_students is deleted
		delete from students s where s.student_id=old.active_students_id;
	
	create trigger delete_graduated_student after delete on graduated_students for each row -- Delete student as well when a graduated_student is deleted
		delete from students s where s.student_id=old.graduated_student_id;
	
	-- ------------------------------------------------
	
	create trigger delete_administrative_staff_employee after delete on administrative_staffs for each row -- Delete employee instance of a administrative_staff upon deletion
		delete from employees where employees.employee_id=old.administrative_staff_id;
	

	create trigger delete_instructor_employee after delete on instructors for each row -- Delete employee instance of a instructor upon deletion
		delete from employees e where e.employee_id=old.instructor_id;
	
		create trigger delete_instructor_available_hours before delete on instructors for each row -- Delete a instructors available hours when he is deleted
			delete from instructor_to_available_hours itah where itah.instructor_id=old.instructor_id;
		
		create trigger delete_instructor_to_courses before delete on instructors for each row -- Delete a instructors teached courses when he is deleted
			delete from instructor_to_courses itc where itc.instructor_id=old.instructor_id;
	
	
	create trigger delete_janitor_employee after delete on janitors for each row -- Delete employee instance of a janitor upon deletion
		delete from employees where employees.employee_id=old.janitor_id;
	
-- ------------------------------------------------
	
	create trigger delete_materials before delete on courses for each row -- Delete materials required by a course upon deletion
		delete from material_to_courses where material_to_courses.course_id=old.course_id;
	
	create trigger delete_course_times before delete on courses for each row -- Delete times of a course upon deletion
		delete from course_times where course_times.course_id=old.course_id;

-- ADD MORE TRIGGERS PERHAPS?


-- Filling the tables and testing

	insert into students (student_id, first_name, last_name, age)
	values
	    (1, 'John', 'Doe', 20),
	    (2, 'Emily', 'Smith', 22),
	    (3, 'Michael', 'Johnson', 21),
	    (4, 'Sophia', 'Williams', 23),
	    (5, 'Daniel', 'Brown', 20),
	    (6, 'Olivia', 'Miller', 21),
	    (7, 'Alexander', 'Davis', 22),
	    (8, 'Emma', 'Garcia', 19),
	    (9, 'William', 'Martinez', 24),
	    (10, 'Ava', 'Lopez', 20),
	    (11, 'James', 'Gonzalez', 21),
	    (12, 'Mia', 'Lee', 23),
	    (13, 'Benjamin', 'Clark', 22),
	    (14, 'Avery', 'Hall', 20),
	    (15, 'Charlotte', 'Young', 19),
	    (16, 'Ethan', 'King', 24),
	    (17, 'Lily', 'Hill', 21),
	    (18, 'Harper', 'Adams', 20),
	    (19, 'Amelia', 'Wright', 22),
	    (20, 'Jack', 'Scott', 23),
	    (21, 'Sophie', 'Green', 19),
	    (22, 'David', 'Baker', 21),
	    (23, 'Grace', 'Evans', 22),
	    (24, 'Logan', 'Morris', 20),
	    (25, 'Nora', 'Rogers', 23);
	   
	insert into active_students (active_student_id)
	values
	    (1),
	    (2);
	    
	insert into courses (course_id, course_name, course_minimum_demand_amount_to_open, course_demand_amount)
	values
	    ('MAT101', 'Mathematics'         , 10, 0),
	    ('ENG102', 'English'             , 10, 0),
	    ('GER103', 'German'              , 10, 0),
	    ('CHS104', 'Chess'               , 10, 0),
	    ('RPG106', 'Robotics_Programming', 10, 0);
	   
	insert into parents (student_id, first_name, last_name, phone_number, email)
	values
	    (1, 'Mete'  , 'Gulsoy', '01234567890', 'mgulsoy@etu.edu.tr'),
	    (2, 'Hasan' , 'Tuna'  , '01234567890', 'htuna@etu.edu.tr'),
	    (3, 'Yusuf' , 'Aydin' , '01234567890', 'yaydin@etu.edu.tr'),
	    (4, 'Ahmed' , 'Kasik' , '01234567890', 'ahmet@etu.edu.tr'),
	    (5, 'Atilla', 'Ak'    , '01234567890', 'atilla@etu.edu.tr')
	;
	 
	insert into employees (employee_id, salary, first_name, last_name)
	values
		(1, 2500, 'Toygar', 'Akgun'),
		(2, 2500, 'Mehmet Burak', 'Akgun')
	;
	
	insert into instructors (instructor_id)
	values
		(1)
	;
	
	insert into instructor_to_available_hours (instructor_id, available_day, an_available_hour)
	values
		(1, 'monday', '13:00:00')
	;
	
	insert into instructor_to_teached_courses (instructor_id, teached_course_id)
	values
		(1, 'MAT101')
	;
	   
   
-- Viewing data

    select * from parents p ;

	delete from active_students where active_student_id=1;

	insert into graduated_students (graduated_student_id)
	values
		(2)
	;

    select * from parents p ;
   
	delete from graduated_students where graduated_student_id=2;

	delete from instructors where instructor_id=1;

   	select * from instructors t;

	
