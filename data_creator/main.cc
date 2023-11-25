// This code generates the data for the project

#include <bits/stdc++.h>
#include <sys/types.h>
#include <unordered_map>

using namespace std;

unordered_map<string, string> id_to_last_name;
unordered_map<string, int> next_id;
int main(){
    freopen("generator.sql", "w", stdout);
    srand(time(NULL));
    fstream file("name_list.txt");
    fstream female_name("first_female_names.txt");
    fstream male_name("first_male_names.txt");

    // Faculties: Engineering: 01, Medicine: 02, Economics: 03

    // Engineering departments: CMP 01, EEE 02, MAE 03
    // Medicine departments: MED 01, DEN 02, PHA 03
    // Economics departments: BUS 01, ECO 02, INR 03
    cout << "INSERT INTO students(student_id, tr_id, first_name, last_name, birth_date) VALUES " << endl;
    vector<string> ids;
    for(int i = 0; i < 400; i++){
        string first_name, last_name;
        int entry_year = rand() % 5 + 19;
        int birth_year = 2023 - 18 - (23 - entry_year);


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

        file >> first_name >> last_name;
        string email = first_name + "." + last_name + "@etu.edu.tr";
        string tr_id;
        for(int i = 0; i < 11; i++){
            tr_id += to_string(rand() % 10);
        }
        id_to_last_name[id] = last_name;
        ids.push_back(id);
        cout << "(" << id << ", " << tr_id << ", '" << first_name << "', '" << last_name << "', '";
        cout << rand() % 28 + 1 << "/" << rand() % 12 + 1 << "/" << birth_year << "')";
        if(i != 499) cout << ",";
        cout << endl;
    }
    cout<< ";" << endl << endl;

    cout << "INSERT INTO active_students(active_student_id) VALUES (" << endl;
    for(string id: ids){
        cout << "(" << id << ")";
        if(id != ids.back()) cout << ",";
        cout << endl;
    }
    cout << ");" << endl << endl;


    cout << "INSERT INTO students(student_id, tr_id, first_name, last_name, birth_date) VALUES " << endl;
    ids.clear();
    for(int i = 0; i < 100; i++){
        string first_name, last_name;
        int entry_year = rand() % 5 + 14;
        int birth_year = 2019 - 18 - (23 - entry_year);
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

        file >> first_name >> last_name;
        string email = first_name + "." + last_name + "@etu.edu.tr";
        string tr_id;
        for(int i = 0; i < 11; i++){
            tr_id += to_string(rand() % 10);
        }
        ids.push_back(id);
        cout << "(" << id << ", " << tr_id << ", '" << first_name << "', '" << last_name << "', '";
        cout << rand() % 28 + 1 << "/" << rand() % 12 + 1 << "/" << birth_year << "')";
        if(i != 499) cout << ",";
        cout << endl;
    }

    cout<< ";" << endl << endl;

    cout << "INSERT INTO graduated_students(graduated_student_id) VALUES (" << endl;
    for(string id: ids){
        cout << "(" << id << ")";
        if(id != ids.back()) cout << ",";
        cout << endl;
    }
    cout << ");" << endl << endl;


    // Create parents 
    cout << "INSERT INTO parents(student_id, tr_id, first_name, last_name, phone_number, email) VALUES " << endl;
    // for every student either put a female parent, male parent or none
    int cnt = 0;
    for(auto p: id_to_last_name){
        string id = p.first;
        string last_name = p.second;
        string first_name;

        if(rand() % 2) // Female
            female_name >> first_name;
        else
            male_name >> first_name;
        string tr_id;
        for(int i = 0; i < 11; i++){
            tr_id += to_string(rand() % 10);
        }

        string phone_number = "05";
        for(int i = 0; i < 9; i++){
            phone_number += to_string(rand() % 10);
        }

        string email = first_name + "." + last_name + "@gmail.com";

        cout << "(" << id << ", " << tr_id << ", '" << first_name << "', '" << last_name << "', '" << phone_number << "', '" << email << "')";
        if(cnt != 499) cout << ",";
        cout << endl;
        cnt++;
    }


    cout << ";" << endl << endl;

    cout << "INSERT INTO EXPENSES(expense_id, expense_name, amount, is_fixed, expense_date) VALUES " << endl;

    // List some expanses for university management
    vector<string> expense_names = {"Electricity", "Water", "Internet", "Gas", "Cleaning", "Security", "Food", "Salary", "Other"};

    for(int i = 0; i < 8; i++){
        string expense_name = expense_names[i];
        int amount = rand() % 1000 + 100;
        int is_fixed = rand() % 2;
        int expense_date = rand() % 28 + 1;
        cout << "(" << i + 1 << ", '" << expense_name << "', " << amount << ", " << is_fixed << ", " << expense_date << ")";
        if(i != 7) cout << ",";
        cout << endl;
    }

    cout << ";" << endl << endl;

    //Create 3 courses for each department

    // cout << "INSERT INTO courses(course_code, course_name, course_minimum_demand_to_open) VALUES " << endl;

    // //GENERATE COURSE CODES IN THE FOLLOWING FORMAT: DEPARTMENT_CODE + COURSE_CODE
    // // Engineering departments: CMP 01, EEE 02, MAE 03
    // // Medicine departments: MED 01, DEN 02, PHA 03
    // // Economics departments: BUS 01, ECO 02, INR 03

    // //GENERATE 3 COURSES FOR EACH DEPARTMENT

    // vector<string> course_codes;

    // for(int i = 0; i < 3; i++){
    //     string course_code = "1";
    //     course_code += "0" + to_string(i + 1);
    //     course_codes.push_back(course_code);
    // }


}