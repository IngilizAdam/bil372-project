# Boolean {table}_sex attributes are 0 for man, 1 for woman
# Boolean empl_type is 0 for, full-time 1 for part time
# Boolean exps_type is 0 for regular expenses, 1 for specific expenses

# Hour attributes has to be between 8 and 17 to enumerate 08.00 to 17.00
# Day attributes has to be between 0 and 6 to enumerate Monday to Sunday

# It may be more convenient to control the data constraints on application since it is 
#   easier to maintain

drop database if exists okul_veri_tabani;
create database okul_veri_tabani;
use okul_veri_tabani;

CREATE TABLE EMPLOYEE (
    empl_id       INT,
    empl_type     BOOL        NOT NULL,
    empl_fname    VARCHAR(12) NOT NULL,
    empl_lname    VARCHAR(12) NOT NULL,
    empl_sex      BOOL        NOT NULL,
    empl_salary   INT         NOT NULL,
    empl_reg_date DATE        NOT NULL,
    
    CHECK (empl_salary >= 0),
    
    PRIMARY KEY(empl_id)
);

CREATE TABLE ADMIN_STAFF (
    empl_id INT,
    
    PRIMARY KEY (empl_id),
    FOREIGN KEY (empl_id) REFERENCES EMPLOYEE(empl_id)
);

CREATE TABLE INSTRUCTOR (
    empl_id INT,
    
    PRIMARY KEY (empl_id),
    FOREIGN KEY (empl_id) REFERENCES EMPLOYEE(empl_id)
);

CREATE TABLE STAFF (
    empl_id INT,
    
    PRIMARY KEY (empl_id),
    FOREIGN KEY (empl_id) REFERENCES EMPLOYEE(empl_id)
);

CREATE TABLE STUDENT (
    stud_id       	INT,
    stud_fname      VARCHAR(12) NOT NULL,
    stud_lname      VARCHAR(12) NOT NULL,
    stud_birth_date DATE,
    stud_sex      	BOOL,
    stud_reg_date 	DATE        NOT NULL,
    stud_gpa      	FLOAT       NOT NULL,
    stud_email    	VARCHAR(32) NOT NULL,
    stud_number   	CHAR(11)    NOT NULL,
    
    CHECK (stud_gpa >= 0 and stud_gpa <= 4),
    
    PRIMARY KEY (stud_id)
);

CREATE TABLE ACTIVE (
    stud_id INT,
    
    PRIMARY KEY (stud_id),
    FOREIGN KEY (stud_id) REFERENCES STUDENT(stud_id)
);

CREATE TABLE GRADUATE (
    stud_id        INT,
    grad_grad_date DATE   NOT NULL,

    PRIMARY KEY (stud_id),
    FOREIGN KEY (stud_id) REFERENCES STUDENT(stud_id)
);

CREATE TABLE PARENT (
    pare_id     INT         AUTO_INCREMENT,
    pare_fname  VARCHAR(12) NOT NULL,
    pare_lname  VARCHAR(12) NOT NULL,
    pare_sex    BOOL,
    pare_email  VARCHAR(32) NOT NULL,
    pare_number CHAR(11)    NOT NULL,
    
    PRIMARY KEY (pare_id)
);

CREATE TABLE EXPENSE (
    exps_id   INT         AUTO_INCREMENT,
    exps_name VARCHAR(12) NOT NULL,
    exps_date DATE        NOT NULL,
    exps_cost INT         NOT NULL,
    exps_type BOOL        NOT NULL,
    
    CHECK (exps_cost >= 0),
    
    PRIMARY KEY (exps_id)
);

CREATE TABLE STOCK (
    stck_id    INT         AUTO_INCREMENT,
    stck_name  VARCHAR(12) NOT NULL,
    stck_count INT         NOT NULL,
    
    CHECK (stck_count >= 0),
    
    PRIMARY KEY (stck_id)
);

CREATE TABLE COURSE (
    cour_id         CHAR(6),
    cour_min_req    INT,
    
    CHECK (cour_min_req >= 0),
    
    PRIMARY KEY (cour_id)
);

-- ----------------------------------------------------------------------------

CREATE TABLE STUD_AVAIL_HOUR (
    stud_id    INT,
    avail_hour INT,
    avail_day  INT,
    
    CHECK (avail_hour >= 0 AND avail_hour <= 23),
    CHECK (avail_day >= 0 AND avail_day <= 6),
    
    PRIMARY KEY (stud_id, avail_hour, avail_day),
    FOREIGN KEY (stud_id) REFERENCES ACTIVE(stud_id) ON DELETE CASCADE
);

CREATE TABLE INST_AVAIL_HOUR (
    empl_id    INT,
    avail_hour INT,
    avail_day  INT,
    
    CHECK (avail_hour >= 0 AND avail_hour <= 23),
    CHECK (avail_day >= 0 AND avail_day <= 6),
    
    PRIMARY KEY (empl_id, avail_hour, avail_day),
    FOREIGN KEY (empl_id) REFERENCES INSTRUCTOR(empl_id) ON DELETE CASCADE
);

CREATE TABLE COUR_HOUR (
    cour_id   CHAR(6),
    cour_hour INT,
    cour_day  INT,
    
    CHECK (cour_hour >= 0 AND cour_hour <= 23),
    CHECK (cour_day >= 0 AND cour_day <= 6),
    
    PRIMARY KEY (cour_id, cour_hour, cour_day),
    FOREIGN KEY (cour_id) REFERENCES COURSE(cour_id) ON DELETE CASCADE
);

CREATE TABLE RELATIVE (
    stud_id   INT,
    pare_id   INT,
    rela_type VARCHAR(16),
    
    PRIMARY KEY (stud_id, pare_id),
    FOREIGN KEY (stud_id) REFERENCES STUDENT(stud_id) ON DELETE CASCADE,
    FOREIGN KEY (pare_id) REFERENCES PARENT(pare_id) ON DELETE CASCADE
);

CREATE TABLE GIVEN_COURSE (
    inst_id INT,
    cour_id CHAR(6),
    
    PRIMARY KEY(cour_id),
    FOREIGN KEY(inst_id) REFERENCES INSTRUCTOR(empl_id) ON DELETE CASCADE,
    FOREIGN KEY(cour_id) REFERENCES COURSE(cour_id) ON DELETE CASCADE
);

CREATE TABLE REQUESTED_COURSE (
    stud_id INT,
    cour_id CHAR(6),
    
    PRIMARY KEY(stud_id, cour_id),
    FOREIGN KEY(stud_id) REFERENCES ACTIVE(stud_id) ON DELETE CASCADE,
    FOREIGN KEY(cour_id) REFERENCES COURSE(cour_id) ON DELETE CASCADE
);

CREATE TABLE TAKEN_COURSE (
    stud_id INT,
    cour_id CHAR(6),
    
    PRIMARY KEY(stud_id, cour_id),
    FOREIGN KEY(stud_id) REFERENCES ACTIVE(stud_id) ON DELETE CASCADE,
    FOREIGN KEY(cour_id) REFERENCES COURSE(cour_id) ON DELETE CASCADE
);

CREATE TABLE NEEDED_STOCK (
    cour_id     CHAR(6),
    stck_id     INT,
    need_amount INT NOT NULL,

    PRIMARY KEY (cour_id, stck_id),        
    FOREIGN KEY (cour_id) REFERENCES COURSE(cour_id) ON DELETE CASCADE,
    FOREIGN KEY (stck_id) REFERENCES STOCK(stck_id) ON DELETE CASCADE
);


-- STUDENT ------------------------------------------------

create trigger DELETE_GRADUATE_STUDENT before DELETE on STUDENT for each row -- DELETE a graduate students student when he is deleted
	DELETE from GRADUATE g where g.stud_id=old.stud_id;

create trigger DELETE_ACTIVE_STUDENT before DELETE on STUDENT for each row -- DELETE a active students student when he is deleted
	DELETE from ACTIVE a where a.stud_id=old.stud_id;


-- EMPLOYEE ------------------------------------------------

create trigger DELETE_INSTRUCTOR_EMPLOYEE before DELETE on EMPLOYEE for each row
	DELETE from INSTRUCTOR i where i.empl_id=old.empl_id;

create trigger DELETE_STAFF_EMPLOYEE before DELETE on EMPLOYEE for each row
	DELETE from STAFF s where s.empl_id=old.empl_id;

create trigger DELETE_ADMIN_STAFF_EMPLOYEE before DELETE on EMPLOYEE for each row
	DELETE from ADMIN_STAFF a where a.empl_id=old.empl_id;