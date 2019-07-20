//Global Vars
var allGroupsArr = [];
var allUsersArr = [];
var allUserInClass = [];
var allCitiesArr = [];
var numOfRegisteredPerEducation = [];
var choosenGroupSmarties = [];
var choosenGroupOk = [];
var choosenGroupFine = [];
var smartPerDay = [{ "Sun": 0 }, { "Mon": 0 }, { "Tue": 0 }, { "Wed": 0 }, { "Thu": 0 }];;//Education=3
var okPerDay = [{ "Sun": 0 }, { "Mon": 0 }, { "Tue": 0 }, { "Wed": 0 }, { "Thu": 0 }];;//Education=2
var finePerDay = [{ "Sun": 0 }, { "Mon": 0 }, { "Tue": 0 }, { "Wed": 0 }, { "Thu": 0 }];//Education=1
var maleArr = [];
var femaleArr = [];
educationTypeFine = 1;
educationTypeOk = 2;
educationTypeSmarties = 3;



//Retrive all groups from DB when document is ready
//*************************************************
$(document).ready(function () {
    ajaxCall("GET", "../api/group/getAllGroups", "", GetAllGroupsSuccess, ErrorGetAllGroups);
    ajaxCall("GET", "../api/city/topFiveCities", "", GetAllCitiesSuccess, ErrorGetAllCities);
    
    
});

//Ajax Call function Error and Success
//*************************************************
function GetAllGroupsSuccess(groupsFromDb)
{
    allGroupsArr = groupsFromDb;
   
    ajaxCall("GET", "../api/user/getAllUser", "", GetAllUsersSuccess, ErrorGetAllUsers);
   
}
function ErrorGetAllGroups() {
    console.log("Error get all groups from DB");
}
function GetAllUsersSuccess(allUsers) {
    allUsersArr = allUsers;
    ajaxCall("GET", "../api/userinclass/getAllUserInClass", "", GetAllUsersInClassSuccess, ErrorGetAllUserInClass);
}
function ErrorGetAllUsers() {
    console.log("Error get all Users from DB");
}
function GetAllUsersInClassSuccess(usersInClass) {
    allUserInClass = usersInClass;
    allGraphs();
    
    
}
function ErrorGetAllUserInClass() {
    console.log("Error get all Users In Class from DB");
}

function GetAllCitiesSuccess(allCities) {
    ajaxCall("GET", "../api/user/numOfRegisteredPerEducation", "", GetAllRegisteredSuccess, ErrorGetAllRegistered);
    allCitiesArr = allCities;
    TopFiveCities();

}
function ErrorGetAllCities() {
    console.log("Error Return All Cities From DB");
}

function GetAllRegisteredSuccess(registered) {
    numOfRegisteredPerEducation = registered;
    educationPie();
}

function ErrorGetAllRegistered() {
    console.log("Error Return All Registered From DB");
}

function allGraphs() {
    devideToGroupsSmarties();
    devideToGroupsOk();
    devideToGroupsFine();
    sumPerDay();
    DevidePerGender();
}

//כאשר לוחצים כל הכפתור 'לחץ להצגת גרף' הפונקציה הזאת תרוץ
//************************************************************
function devideToGroupsSmarties() {
    
    for (var i = 0; i < allGroupsArr.length; i++) {
        if (allGroupsArr[i].Education == educationTypeSmarties) {
            choosenGroupSmarties.push(allGroupsArr[i]);
           
        }
    }
    let groupFinish = 0;
    let groupNotFinish = 0;
    for (var i = 0; i < choosenGroupSmarties.length; i++) {
        if (choosenGroupSmarties[i].IsFinished == true)
            groupFinish += 1;
        else
            groupNotFinish += 1;
    }
    let finishPercentage = (groupFinish / choosenGroupSmarties.length) * 100;
    let notFinishPercentage = (groupNotFinish / choosenGroupSmarties.length) * 100;
    GroupsPieSmarties(finishPercentage, notFinishPercentage)
}

function devideToGroupsFine() {
    for (var i = 0; i < allGroupsArr.length; i++) {
        if (allGroupsArr[i].Education == educationTypeFine) {
            choosenGroupFine.push(allGroupsArr[i]);

        }
    }
    let groupFinish = 0;
    let groupNotFinish = 0;
    for (var i = 0; i < choosenGroupFine.length; i++) {
        if (choosenGroupFine[i].IsFinished == true)
            groupFinish += 1;
        else
            groupNotFinish += 1;
    }
    let finishPercentage = (groupFinish / choosenGroupFine.length) * 100;
    let notFinishPercentage = (groupNotFinish / choosenGroupFine.length) * 100;
    GroupsPieFine(finishPercentage, notFinishPercentage)
}

function devideToGroupsOk() {
    
    for (var i = 0; i < allGroupsArr.length; i++) {
        if (allGroupsArr[i].Education == educationTypeOk) {
            choosenGroupOk.push(allGroupsArr[i]);

        }
    }
    let groupFinish = 0;
    let groupNotFinish = 0;
    for (var i = 0; i < choosenGroupOk.length; i++) {
        if (choosenGroupOk[i].IsFinished == true)
            groupFinish += 1;
        else
            groupNotFinish += 1;
    }
    let finishPercentage = (groupFinish / choosenGroupOk.length) * 100;
    let notFinishPercentage = (groupNotFinish / choosenGroupOk.length) * 100;
    GroupsPieOk(finishPercentage, notFinishPercentage)
}
//*************************************************************************************
function sumPerDay() {

    for (var i = 0; i < allGroupsArr.length; i++) {
        if (allGroupsArr[i].IsFinished == false && allGroupsArr[i].Education == 1) {
            switch (allGroupsArr[i].Day1) {
                case 1:
                    finePerDay[0]["Sun"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 2:
                    finePerDay[1]["Mon"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 3:
                    finePerDay[2]["Tue"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 4:
                    finePerDay[3]["Wed"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 5:
                    finePerDay[4]["Thu"] += allGroupsArr[i].Num_Of_Registered;
                    break;
            }

        }
        else if (allGroupsArr[i].IsFinished == false && allGroupsArr[i].Education == 2) {
            switch (allGroupsArr[i].Day1) {
                case 1:
                    okPerDay[0]["Sun"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 2:
                    okPerDay[1]["Mon"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 3:
                    okPerDay[2]["Tue"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 4:
                    okPerDay[3]["Wed"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 5:
                    okPerDay[4]["Thu"] += allGroupsArr[i].Num_Of_Registered;
                    break;
            }
        }
        else if (allGroupsArr[i].IsFinished == false && allGroupsArr[i].Education == 3) {
            switch (allGroupsArr[i].Day1) {
                case 1:
                    smartPerDay[0]["Sun"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 2:
                    smartPerDay[1]["Mon"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 3:
                    smartPerDay[2]["Tue"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 4:
                    smartPerDay[3]["Wed"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 5:
                    smartPerDay[4]["Thu"] += allGroupsArr[i].Num_Of_Registered;
                    break;
            }
        }
    }
    BarChart1();
}

//יצירת גרף פאי על פי השכלה של כל המסיימים והלא מסיימים
    function GroupsPieSmarties(finishPercentage, notFinishPercentage) {
        var chart = new CanvasJS.Chart("FinishAnalysisSmarties", {
            animationEnabled: true,
            //title: {
            //    text: "שיעור מסיימי הקורס מכלל הנרשמים בקרב אקדמאים"
            //},
            data: [{
                type: "pie",
                startAngle: 240,
                yValueFormatString: "##0.00\"%\"",
                indexLabel: "{label} {y}",
                dataPoints: [
                    { y: finishPercentage, label: "סיימו" },
                    { y: notFinishPercentage, label: "לא סיימו" }


                ]
            }]
        });
        chart.render();
}
function GroupsPieOk(finishPercentage, notFinishPercentage) {
    var chart = new CanvasJS.Chart("FinishAnalysisOk", {
        animationEnabled: true,
        //title: {
        //    text: "שיעור מסיימי הקורס מכלל הנרשמים בקרב בעלי השכלה תיכונית"
        //},
        data: [{
            type: "pie",
            startAngle: 240,
            yValueFormatString: "##0.00\"%\"",
            indexLabel: "{label} {y}",
            dataPoints: [
                { y: finishPercentage, label: "סיימו" },
                { y: notFinishPercentage, label: "לא סיימו" }


            ]
        }]
    });
    chart.render();
}
function GroupsPieFine(finishPercentage, notFinishPercentage) {
    var chart = new CanvasJS.Chart("FinishAnalysisFine", {
        animationEnabled: true,
        //title: {
        //    text: "שיעור מסיימי הקורס מכלל הנרשמים בקרב בעלי השכלה בסיסית"
        //},
        data: [{
            type: "pie",
            startAngle: 240,
            yValueFormatString: "##0.00\"%\"",
            indexLabel: "{label} {y}",
            dataPoints: [
                { y: finishPercentage, label: "סיימו" },
                { y: notFinishPercentage, label: "לא סיימו" }


            ]
        }]
    });
    chart.render();
}


//יצירת בר גרף על פי קבוצות בכל יום
function BarChart1() {

    var chart = new CanvasJS.Chart("RegisteredAnalysis", {
        animationEnabled: true,
        
        axisY: {
            title: "Number Of Registered"
        },
        legend: {
            cursor: "pointer",
            itemclick: toggleDataSeries
        },
        toolTip: {
            shared: true,
            content: toolTipFormatter
        },
        data: [{
            type: "bar",
            showInLegend: true,
            name: "השכלה אקדמית",
            color: "#007bff",
            dataPoints: [
                { y: smartPerDay[0]["Sun"], label: "ראשון" },
                { y: smartPerDay[1]["Mon"], label: "שני" },
                { y: smartPerDay[2]["Tue"], label: "שלישי" },
                { y: smartPerDay[3]["Wed"], label: "רביעי" },
                { y: smartPerDay[4]["Thu"], label: "חמישי" }
                
            ]
        },
        {
            type: "bar",
            showInLegend: true,
            name: "השכלה תיכונית",
            color: "#dc3545",
            dataPoints: [
                { y: okPerDay[0]["Sun"], label: "ראשון" },
                { y: okPerDay[1]["Mon"], label: "שני" },
                { y: okPerDay[2]["Tue"], label: "שלישי" },
                { y: okPerDay[3]["Wed"], label: "רביעי" },
                { y: okPerDay[4]["Thu"], label: "חמישי" }
            ]
        },
        {
            type: "bar",
            showInLegend: true,
            name: "ללא השכלה/ השכלה מינימלית",
            color: "#20c997",
            dataPoints: [
                { y: finePerDay[0]["Sun"], label: "ראשון" },
                { y: finePerDay[1]["Mon"], label: "שני" },
                { y: finePerDay[2]["Tue"], label: "שלישי" },
                { y: finePerDay[3]["Wed"], label: "רביעי" },
                { y: finePerDay[4]["Thu"], label: "חמישי" }
            ]
        }]
    });
    chart.render();

    function toolTipFormatter(e) {
        var str = "";
        var total = 0;
        var str3;
        var str2;
        for (var i = 0; i < e.entries.length; i++) {
            var str1 = "<span style= \"color:" + e.entries[i].dataSeries.color + "\">" + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
            total = e.entries[i].dataPoint.y + total;
            str = str.concat(str1);
        }
        str2 = "<strong>" + e.entries[0].dataPoint.label + "</strong> <br/>";
        str3 = "<span style = \"color:Tomato\">Total: </span><strong>" + total + "</strong><br/>";
        return (str2.concat(str)).concat(str3);
    }

    function toggleDataSeries(e) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
        }
        else {
            e.dataSeries.visible = true;
        }
        chart.render();
    }

}   

function DevidePerGender() {
    let gender = 0;
    for (var i = 0; i < allUsersArr.length; i++) {
        gender = parseInt(allUsersArr[i].Gender);
        switch (gender) {
            case 1:
                maleArr.push(allUsersArr[i].Gender);
                break;
            case 2:
                femaleArr.push(allUsersArr[i].Gender);
                break;
        }
    }
    drillDownPerGender();
}

function drillDownPerGender() {
    

    var chart = new CanvasJS.Chart("drillDown1", {
        animationEnabled: true,
        data: [{
            type: "doughnut",
            startAngle: 60,
            //innerRadius: 60,
            indexLabelFontSize: 17,
            indexLabel: "{label} - #percent%",
            toolTipContent: "<b>{label}:</b> {y} (#percent%)",
            dataPoints: [
                { y: maleArr.length, label:'גברים' },
                { y: femaleArr.length, label:'נשים'}
               
            
            ]
        }]
    });
    chart.render();
   
}
//******************************Top 5 Cities**************************//

function TopFiveCities() {

    var chart = new CanvasJS.Chart("Top5", {
        animationEnabled: true,
        theme: "light2", // "light1", "light2", "dark1", "dark2"
       
        axisY: {
            title: "Reserves(MMbbl)"
        },
        data: [{
            type: "column",
            showInLegend: true,
            legendMarkerColor: "grey",
            legendText: "MMbbl = one million barrels",
            dataPoints: [
                { y: allCitiesArr[0].NumOfUsers, label: allCitiesArr[0].CityName },
                { y: allCitiesArr[1].NumOfUsers, label: allCitiesArr[1].CityName },
                { y: allCitiesArr[2].NumOfUsers, label: allCitiesArr[2].CityName },
                { y: allCitiesArr[3].NumOfUsers, label: allCitiesArr[3].CityName },
                /*{ y: allCitiesArr[4].NumOfUsers, label: allCitiesArr[4].CityName }*/
            
            ]
        }]
    });
    chart.render();

}

function educationPie() {
    
    var chart = new CanvasJS.Chart("educationPie", {
        animationEnabled: true,
        data: [{
            type: "doughnut",
            startAngle: 60,
            //innerRadius: 60,
            indexLabelFontSize: 17,
            indexLabel: "{label} - #percent%",
            toolTipContent: "<b>{label}:</b> {y} (#percent%)",
            dataPoints: [
                { y: numOfRegisteredPerEducation[0].NumOfRegistered, label: numOfRegisteredPerEducation[0].Education_Name},
                { y: numOfRegisteredPerEducation[1].NumOfRegistered, label: numOfRegisteredPerEducation[1].Education_Name },
                { y: numOfRegisteredPerEducation[2].NumOfRegistered, label: numOfRegisteredPerEducation[2].Education_Name },
                /*{ y: numOfRegisteredPerEducation[3].NumOfRegistered, label: numOfRegisteredPerEducation[3].Education_Name }*/
            ]
        }]
    });
    chart.render();

}
