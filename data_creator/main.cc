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

    // Engineering departments: CMP 01, EEE 02, MAE 03
    // Medicine departments: MED 01, DEN 02, PHA 03
    // Economics departments: BUS 01, ECO 02, INR 03
    cout << "INSERT INTO STUDENT(stud_id, stud_fname, stud_lname, stud_birth_year, stud_sex, stud_reg_date, stud_gpa, stud_email, stud_number) VALUES " << endl;
    vector<string> ids;
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
        ids.push_back(id);
        cout << "(" << id << ", '" << first_name << "', '" << last_name << "', '";
        cout<< birth_date << "', " << sex << ", '" << reg_date << "', " << gpa << ", '" << email << "', '" << phone_number << "')"; 
        if(i != 499) cout << ",";
        cout << endl;
    }
    cout<< ";" << endl << endl;

    cout << "INSERT INTO ACTIVE(stud_id) VALUES " << endl;
    for(string id: ids){
        cout << "(" << id << ")";
        if(id != ids.back()) cout << ",";
        cout << endl;
    }
    cout << ";" << endl << endl;


    cout << "INSERT INTO STUDENT(stud_id, stud_fname, stud_lname, stud_birth_year, stud_reg_date, stud_gpa, stud_email, stud_number) VALUES " << endl;
    ids.clear();
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

    cout << "INSERT INTO EXPENSE(exps_id, exps_name, exps_date, exps_cost, exps_type) VALUES " << endl;

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
            cout << "(" << i*4 + j << ", '" << expense_name << "', '" << date << "', " << amount << ", " << is_fixed << ")";

            if(i != 7 || j != 3) cout << ",";
            cout << endl;
        }
    }

    cout << ";" << endl << endl;


    cout << "INSERT INTO EMPLOYEE(empl_id, empl_type, empl_fname, empl_lname, empl_salary, empl_reg_date) VALUES " << endl;

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
        string tr_id;
        for(int i = 0; i < 11; i++){
            tr_id += to_string(rand() % 10);
        }

        surname >> last_name;

        cout << "(" << i + 1 << ", " << rand() % 2 << ", '" << first_name << "', '" << last_name << "', " << salary << ", '";
        cout << to_string(2015 + rand() % 8) << "/" << rand() % 9 + 1 << "/" << rand() % 28 + 1 << "')";
        if(i != 99) cout << ",";
        cout << endl;
    }


    cout << ";" << endl << endl;

    //Insert into employee_type janitor or administrative staff

    cout << "INSERT INTO STAFF(empl_id) VALUES " << endl;
    for(auto emp: employee_ids){
        if(emp.second == 1){
            cout << "(" << emp.first << ")";
            if(emp.first != employee_ids.back().first) cout << ",";
            cout << endl;
        }
    }

    cout << ";" << endl << endl;

    cout << "INSERT INTO ADMIN_STAFF(empl_id) VALUES " << endl;
    for(auto emp: employee_ids){
        if(emp.second == 0){
            cout << "(" << emp.first << ")";
            if(emp.first != employee_ids.back().first) cout << ",";
            cout << endl;
        }
    }


    // cout << ";" << endl << endl;

    // // Create hours for timetable

    // cout << "INSERT INTO hours(time_id, timetable_day, timetable_hour) VALUES " << endl;

    // for(int i = 0; i < 7; i++){
    //     for(int j = 0; j < 24; j++){
    //         cout<< "(" << i * 24 + j << ", " << i << ", " << j << "), \n";
    //     }
    // }

    cout << ";" << endl << endl;

}