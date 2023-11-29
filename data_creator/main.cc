// This code generates the data for the project

#include <bits/stdc++.h>
#include <cstdlib>
#include <iomanip>
#include <iostream>
#include <string>
#include <sys/types.h>
#include <unordered_map>

using namespace std;

unordered_map<string, string> id_to_last_name;
unordered_map<string, int> next_id;
int main(){
    freopen("generator.sql", "w", stdout);
    cout << fixed << setprecision(2);
    srand(time(NULL));
    fstream female_name("first_female_names.txt");
    fstream male_name("first_male_names.txt");
    fstream surname("last_name.txt");

    // Faculties: Engineering: 01, Medicine: 02, Economics: 03

    // Engineering departments: BIL 01, ELE 02, MAK 03
    // Science and Literature: IDE 01, TDE 02, TAR 03
    // Economics departments: ISL 01, IKT 02, SUI 03
    cout << "INSERT INTO STUDENT(stud_id, stud_fname, stud_lname, stud_birth_date, stud_sex, stud_reg_date, stud_gpa, stud_email, stud_number) VALUES " << endl;
    vector<string> ac_ids;
    for(int i = 0; i < 500; i++){
        string first_name, last_name;
        int entry_year = rand() % 5 + 18;
        int birth_year = 2023 - 18 - (23 - entry_year) - rand() % 5;


        string id = to_string(entry_year);
        int fac = rand() % 3 + 1;
        int dep = rand() % 3 + 1;
        id += "0" + to_string(fac) + "0" + to_string(dep);
        if(next_id[id] == 0) 
            next_id[id] = 1;
        else 
            next_id[id]++;
        int val = next_id[id];
        if(val < 10) id += "0";
        id += to_string(val);

        // Create a random date for registration with reg year being same as entry_year
        int reg_day = rand() % 28 + 1;
        int reg_month = rand() % 9 + 1;
        string reg_date = "20" + to_string(entry_year) + "/" + to_string(reg_month) + "/" + to_string(reg_day);

        // Create a random date for birthdate
        int birth_day = rand() % 28 + 1;
        int birth_month = rand() % 12 + 1;
        string birth_date = to_string(birth_year) + "/" + to_string(birth_month) + "/" + to_string(birth_day);

        //Create gpa 

        double gpa = (rand() % 400) / 100.0;

        bool sex = rand() % 2;
        if(sex == 0){
            male_name >> first_name;
        }else{
            female_name >> first_name;
        }

        surname >> last_name;
        string email = first_name + "." + last_name + "@etu.edu.tr";


        string phone_number = "05";
        for(int i = 0; i < 9; i++){
            phone_number += to_string(rand() % 10);
        }

        id_to_last_name[id] = last_name;
        ac_ids.push_back(id);
        cout << "(" << id << ", '" << first_name << "', '" << last_name << "', '";
        cout<< birth_date << "', " << sex << ", '" << reg_date << "', " << gpa << ", '" << email << "', '" << phone_number << "')"; 
        if(i != 499) cout << ",";
        cout << endl;
    }
    cout<< ";" << endl << endl;

    cout << "INSERT INTO ACTIVE(stud_id) VALUES " << endl;
    for(string id: ac_ids){
        cout << "(" << id << ")";
        if(id != ac_ids.back()) cout << ",";
        cout << endl;
    }
    cout << ";" << endl << endl;


    cout << "INSERT INTO STUDENT(stud_id, stud_fname, stud_lname, stud_birth_date, stud_sex, stud_reg_date, stud_gpa, stud_email, stud_number) VALUES " << endl;
    vector<string> ids;
    for(int i = 0; i < 100; i++){
        string first_name, last_name;
        int entry_year = rand() % 5 + 13;
        int birth_year = 2023 - 18 - (23 - entry_year) - rand() % 5;


        string id = to_string(entry_year);
        int fac = rand() % 3 + 1;
        int dep = rand() % 3 + 1;
        id += "0" + to_string(fac) + "0" + to_string(dep);
        if(next_id[id] == 0) 
            next_id[id] = 1;
        else 
            next_id[id]++;
        int val = next_id[id];
        if(val < 10) id += "0";
        id += to_string(val);

        // Create a random date for registration with reg year being same as entry_year
        int reg_day = rand() % 28 + 1;
        int reg_month = rand() % 9 + 1;
        string reg_date = "20" + to_string(entry_year) + "/" + to_string(reg_month) + "/" + to_string(reg_day);

        // Create a random date for birthdate
        int birth_day = rand() % 28 + 1;
        int birth_month = rand() % 12 + 1;
        string birth_date = to_string(birth_year) + "/" + to_string(birth_month) + "/" + to_string(birth_day);

        //Create gpa 

        double gpa = (rand() % 400) / 100.0;

        bool sex = rand() % 2;
        if(sex == 0){
            male_name >> first_name;
        }else{
            female_name >> first_name;
        }

        surname >> last_name;
        string email = first_name + "." + last_name + "@etu.edu.tr";


        string phone_number = "05";
        for(int i = 0; i < 9; i++){
            phone_number += to_string(rand() % 10);
        }

        id_to_last_name[id] = last_name;
        ids.push_back(id);
       cout << "(" << id << ", '" << first_name << "', '" << last_name << "', '";
        cout<< birth_date << "', " << sex << ", '" << reg_date << "', " << gpa << ", '" << email << "', '" << phone_number << "')"; 
        if(i != 99) cout << ",";
        cout << endl;
    }

    cout<< ";" << endl << endl;

    cout << "INSERT INTO GRADUATE(stud_id, grad_grad_date) VALUES " << endl;
    for(string id: ids){
        cout << "(" << id;

        int entry_year = stoi(id.substr(0, 2));
        int grad_year = entry_year + 4 + rand() % 2;
        int grad_day = rand() % 28 + 1;
        int grad_month = rand() % 9 + 1;
        string grad_date = "20" + to_string(grad_year) + "/" + to_string(grad_month) + "/" + to_string(grad_day);
        cout << ", '" << grad_date << "')";
        if(id != ids.back()) cout << ",";
        cout << endl;
    }
    cout << ";" << endl << endl;


    // Create parents 
    cout << "INSERT INTO PARENT(pare_id, pare_fname, pare_lname, pare_sex, pare_email, pare_number) VALUES " << endl;
    // for every student either put a female parent, male parent or none
    int cnt = 0;
    vector<pair<string, pair<string, string>>> parent_to_student;
    for(auto p: id_to_last_name){
        string id = p.first;
        string last_name = p.second;
        string first_name;

        bool sex = rand() % 2;

        if(sex == 1) // Female
            female_name >> first_name;
        else
            male_name >> first_name;

        string type;
        if(rand() % 2)
            type = "Sibling";
        else 
            type = (sex == 1 ? "Mother" : "Father");
        parent_to_student.push_back({type, {id, to_string(cnt + 1)}});
        string tr_id;


        string phone_number = "05";
        for(int i = 0; i < 9; i++){
            phone_number += to_string(rand() % 10);
        }

        string email = first_name + "." + last_name + "@gmail.com";

        cout << "(" << cnt + 1 << ", '" << first_name << "', '" << last_name << "', " << sex << ", '" << email << "', '" << phone_number << "'" << ")";
        if(cnt != 599) cout << ",";
        cout << endl;
        cnt++;
    }


    cout << ";" << endl << endl;

    cout << "INSERT INTO RELATIVE(stud_id, pare_id, rela_type) VALUES " << endl;
    for(auto p: parent_to_student){
        cout << "(" << p.second.first << ", " << p.second.second << ", '" << p.first << "')";
        if(p != parent_to_student.back()) cout << ",";
        cout << endl;
    }

    cout << ";" << endl << endl;

    cout << "INSERT INTO EXPENSE(exps_name, exps_date, exps_cost, exps_type) VALUES " << endl;

    // List some expanses for university management
    vector<string> expense_names = {"Electricity", "Water", "Internet", "Gas", "Cleaning", "Security", "Food", "Salary", "Other"};

    for(int i = 0; i < 8; i++){
        for(int j = 0; j < 4; j++){
            string expense_name = expense_names[i];
            int amount = rand() % 1000 + 100;
            int is_fixed = rand() % 2;
            int expense_date = rand() % 28 + 1;
            int expense_month = rand() % 9 + 1;
            string date = to_string(2020 + j) + "/" + to_string(expense_month) + "/" + to_string(expense_date);
            cout << "(" << "'" << expense_name << "', '" << date << "', " << amount << ", " << is_fixed << ")";

            if(i != 7 || j != 3) cout << ",";
            cout << endl;
        }
    }

    cout << ";" << endl << endl;


    cout << "INSERT INTO EMPLOYEE(empl_id, empl_type, empl_fname, empl_lname, empl_sex, empl_salary, empl_reg_date) VALUES " << endl;

    // Create employees
    vector<pair<string, int>> employee_ids;
    for(int i = 0; i < 100; i++){
        employee_ids.push_back({to_string(i + 1), rand() % 2});
        string first_name, last_name;
        int salary; 
        if(employee_ids.back().second)
            salary = 100 * (rand() % 100) + 1000;
        else
            salary = 100 * (rand() % 100) + 10000;

        bool sex = rand() % 2;

        if(sex == 1) // Female
            female_name >> first_name;
        else
            male_name >> first_name;
        string email = first_name + "." + last_name + "@etu.edu.tr";

        surname >> last_name;

        cout << "(" << i + 1 << ", " << rand() % 2 << ", '" << first_name << "', '" << last_name << "', " <<  sex << ", " << salary << ", '";
        cout << to_string(2015 + rand() % 8) << "/" << rand() % 9 + 1 << "/" << rand() % 28 + 1 << "')";
        if(i != 99) cout << ",";
        cout << endl;
    }


    cout << ";" << endl << endl;

    //Insert into employee_type janitor or administrative staff
    int cnt3 = 0;
    cout << "INSERT INTO STAFF(empl_id) VALUES " << endl;
    cnt3 = 0;
    for(auto emp: employee_ids){
        if(emp.second == 1){
            if(cnt3 != 0)
                cout<<","<<endl;
            cout << "(" << emp.first << ")"; 
            cnt3++;
        }       
    }
    cout << ";" << endl << endl;

    cout << "INSERT INTO ADMIN_STAFF(empl_id) VALUES " << endl;
    cnt3 = 0;
    for(auto emp: employee_ids){
        if(emp.second == 0){
            if(cnt3 != 0)
                cout<<","<<endl;
            cout << "(" << emp.first << ")"; 
            cnt3++;
        }       
    }


    cout << ";" << endl << endl;

    // // Create hours for timetable

    // cout << "INSERT INTO hours(time_id, timetable_day, timetable_hour) VALUES " << endl;

    // for(int i = 0; i < 7; i++){
    //     for(int j = 0; j < 24; j++){
    //         cout<< "(" << i * 24 + j << ", " << i << ", " << j << "), \n";
    //     }
    // }

    // cout << ";" << endl << endl;

    // Hard code the courses

    cout << "INSERT INTO COURSE(cour_id, cour_min_req) VALUES " << endl;
    
    cout << "('BIL113',	30),"<< endl<<
            "('BIL121',	40),"<< endl<<
            "('BIL132',	30),"<< endl<<
            "('BIL141',	40),"<< endl<<
            "('BIL142',	50),"<< endl<<
            "('BIL191',	30),"<< endl<<
            "('BIL211',	30),"<< endl<<
            "('ELE101',	30),"<< endl<<
            "('FIZ101',	50),"<< endl<<
            "('FIZ102',	50),"<< endl<<
            "('HUK115',	20),"<< endl<<
            "('IDE100',	20),"<< endl<<
            "('IDE103',	20),"<< endl<<
            "('IDE104',	20),"<< endl<<
            "('IDE105',	20),"<< endl<<
            "('IDE108',	20),"<< endl<<
            "('IDE110',	20),"<< endl<<
            "('IKT105',	50),"<< endl<<
            "('IKT110',	20),"<< endl<<
            "('ING001',	50),"<< endl<<
            "('ING002',	50),"<< endl<<
            "('ISL113',	20),"<< endl<<
            "('MAK104',	30),"<< endl<<
            "('MAK112',	30),"<< endl<<
            "('MAT101',	50),"<< endl<<
            "('MAT102',	50),"<< endl<<
            "('MAT103',	50),"<< endl<<
            "('MAT104',	20),"<< endl<<
            "('OEG101',	50),"<< endl<<
            "('SUI101',	20),"<< endl<<
            "('SUI102',	20),"<< endl<<
            "('SUI105',	20),"<< endl<<
            "('SUI106',	20),"<< endl<<
            "('SUI108',	20),"<< endl<<
            "('SUI114',	20),"<< endl<<
            "('TAR113',	20),"<< endl<<
            "('TAR131',	20),"<< endl<<
            "('TAR141',	20),"<< endl<<
            "('TAR146',	20),"<< endl<<
            "('TAR191',	20),"<< endl<<
            "('TAR192',	20),"<< endl<<
            "('TDE111',	20),"<< endl<<
            "('TDE121',	20),"<< endl<<
            "('TDE131',	20),"<< endl<<
            "('TDE143',	20),"<< endl<<
            "('TUR101',	50),"<< endl<<
            "('TUR102',	50);";


    cout << endl << endl;

    // Create Instructors in following numbers: 7 for BIL, 5 for ELE, 5 for MAK, 5 for IDE, 5 for TDE, 5 for TAR, 5 for TUR, 5 for ISL, 5 for IKT, 5 for SUI
    // Instructor ID will always begin with a 01 followed by faculty number followed by department number followed by a 2 digit number
    int nums[3][3] = {{4, 2, 2}, {3, 4}, {1, 2, 4}};

    vector<string> inst_ids;
    cout << "INSERT INTO EMPLOYEE(empl_id, empl_type, empl_fname, empl_lname, empl_sex, empl_salary, empl_reg_date) VALUES " << endl;
    for(int idx1 = 0; idx1 < 3; idx1++){
        for(int idx2 = 0; idx2 < 3; idx2++){
            for(int i = 0; i < nums[idx1][idx2]; i++){
 
                string first_name;
                string last_name;
                bool sex = rand() % 2;

                if(sex == 0){
                    male_name >> first_name; 
                }else{
                    female_name >> first_name;
                }

                surname >> last_name;

                string id = to_string(idx1 + 1) + "0" + to_string(idx2 + 1) + "0" +  to_string(i + 1);
                inst_ids.push_back(id);
                int salary = (200 + rand() % 100) * 100;
                cout << "(" << id << ", " << rand() % 2 << ", '" << first_name << "', '" << last_name << "', " << sex << ", " << salary << ", '";
                cout << to_string(2015 + rand() % 8) << "/" << rand() % 9 + 1 << "/" << rand() % 28 + 1 << "')";
                if(idx1 != 2 || idx2 != 2 || i != nums[idx1][idx2] - 1)
                     cout << ",";
                cout << endl; 
            }
        }
    }

    cout <<";" << endl;

    cout << "INSERT INTO INSTRUCTOR(empl_id) VALUES" <<endl;

    for(string id: inst_ids){
        cout<< "(" << id <<")";
        if(id != inst_ids.back())
            cout<<",";
        cout<<endl;
    }

    cout << ";" << endl<<endl;

    cout << "INSERT INTO INST_AVAIL_HOUR(empl_id, avail_hour, avail_day) VALUES" <<endl;
    bool started = 0;
    for(string id: inst_ids){
        for(int i = 0; i < 6; i++){
            for(int j = 8; j <= 20; j++){
                if(rand() % 4){
                    if(started) cout <<","<<endl;
                    started = 1;
                    cout << "(" << id << ", " << j << ", " << i << ")";
                }
            }
        }
    }
    cout<<";"<<endl<<endl;

    cout << "INSERT INTO STUD_AVAIL_HOUR(stud_id, avail_hour, avail_day) VALUES" <<endl;
    started = 0;
    for(string id: ac_ids){
        for(int i = 0; i < 6; i++){
            for(int j = 8; j <= 20; j++){
                if(rand() % 4){
                    if(started) cout <<","<<endl;
                    started = 1;
                    cout << "(" << id << ", " << j << ", " << i << ")";
                }
            }
        }
    }
    cout<<";"<<endl<<endl;


}