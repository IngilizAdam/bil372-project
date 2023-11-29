// This code generates the data for the project

#include <bits/stdc++.h>
#include <cstdlib>
#include <iomanip>
#include <iostream>
#include <string>
#include <sys/types.h>
#include <unordered_map>

using namespace std;

#define ff first
#define ss second

unordered_map<string, string> id_to_last_name;
unordered_map<string, int> next_id;
bool adj_mat[100][100];
int deg[100];
int main(){
    freopen("scheduler.sql", "w", stdout);
    cout << fixed << setprecision(2);
    srand(time(NULL));
    fstream course("courses.txt");
    vector<string> courses;

    string line;
    while (getline(course, line)){
        courses.push_back(line);
    }

    fstream adj("adj_mat.txt");
    vector<int> cours_ids;
    for(int i = 0; i < courses.size(); i++){
        cours_ids.push_back(i);
        for(int j = i; j < courses.size(); j++){
            adj >> adj_mat[i][j];
            adj_mat[j][i] = adj_mat[i][j];
        }
    }
    for(int i = 0; i < courses.size(); i++){
        for(int j = 0; j < courses.size(); j++){
            if(adj_mat[i][j]){
                deg[i]++;
            }
        }
    }

    sort(cours_ids.begin(), cours_ids.end(), [&](int a, int b){
        return deg[a] > deg[b];
    });

    vector<pair<int, pair<int, int>>> courses_time;


    for(int cid: cours_ids){
        int day = rand() % 6;
        for(int i = 0; i < 3; i++){
            int start = 8 + rand() % 11;
            int day = rand() % 6;
            bool flag = true;
            for(auto u: courses_time){
                if(adj_mat[u.ff][cid] == 0 && courses[u.ff].substr(0, 3) != courses[cid].substr(0, 3))
                    continue;
                if(u.ss.ff == day && u.ss.ss == start){
                    flag = false;
                    break;
                }
            }

            if(flag == false){
                i--;
                continue;
            }
            courses_time.push_back({cid, {day, start}});
            day = (day + 1) % 6;
        }

    }

    cout << "INSERT INTO COUR_HOUR(cour_id, cour_hour, cour_day) VALUES\n";
    for(int i = 0; i < courses_time.size(); i++){
        cout << "('" << courses[courses_time[i].ff] << "', " << courses_time[i].ss.ss << ", " << courses_time[i].ss.ff << ")";
        if(i != courses_time.size() - 1)
            cout << ",\n";
        else
            cout << ";\n";
    }


 
}