﻿


dragula([
    document.getElementById('1'),
    document.getElementById('2'),
    document.getElementById('3'),
    document.getElementById('4'),
    document.getElementById('5'),
    document.getElementById('6')]).


    on('drag', function (el) {

        // add 'is-moving' class to element being dragged
        el.classList.add('is-moving');
    }).
    on('dragend', function (el) {

        // remove 'is-moving' class from element after dragging has stopped
        el.classList.remove('is-moving');

        // add the 'is-moved' class for 600ms then remove it
        window.setTimeout(function () {
            el.classList.add('is-moved');
            window.setTimeout(function () {
                el.classList.remove('is-moved');
            }, 600);
        }, 100);
    });
//***********************************************************************************************************************
var counter = 305;
var section_status = 2;
var readySectionsArr = [];
var generalSectionsArr = [];
var sNotApproved = [];
var sApprovedOrder = [];
var classMaxId;
var classMaxVersion;



//Template for Section
function Section_Json(id, title, content, status,position,class_version,class_id) {
        this.Id =id ,
        this.Title = title,
        this.Description = content,
        //this.Image = image,
        this.Status = status,
        this.Position = position,
        this.Version = class_version,
        this.ClassId = class_id
        
}
//הוספת אובייקט ג'ייסון של מקטע חדש לתוך מערך המקטעים הכללי

function AddSection() {
    section_status = 2;
    var title = document.getElementById("section-title").value;
    var content = document.getElementById("section-content").value;
    var image = document.getElementById("section-image").value;
    var ready = document.getElementById("ready").checked;

    if (ready === true) {
        section_status = 3;
    }


    //Saving the section details in JSON object
    Section_Json.Id = counter;
    Section_Json.Description = content;
    Section_Json.Title = title;
    //Section_Json.Image = image;
    Section_Json.Status = section_status;
    Section_Json.Position = -1;
    Section_Json.Version = classMaxVersion,
    Section_Json.ClassId = classMaxId +1
    var sec = new Section_Json(Section_Json.Id, Section_Json.Title, Section_Json.Description, Section_Json.Status, Section_Json.Position, Section_Json.Version, Section_Json.ClassId);

    var list = document.getElementById(section_status);
    temp = list.innerHTML;
    temp = temp + "<li id=" + counter + " class='drag-item' style='position:relative;text-align:right'> " + title + "<img src='../Images/trash.png' onclick='Delete(this)' style='width:20px;height:20px;margin:5px;position:absolute;top:2px;left:1px' /></li > ";
    list.innerHTML = temp;
    counter++;
    generalSectionsArr.push(sec);
    $("#squarespaceModal").modal("hide");
}


//Delete specific section

function Delete(e) {
    e.parentNode.parentNode.removeChild(e.parentNode);
}

var notReady;
var waiting;
var ready;


//Printing the sections one by one

/***************************Get Last Id And Save Class*****************************************************/


//Error and Success function receiving class Max ID
function GetClassIDSuccess(classId) {
    
    
    classMaxId = classId.Id;
    classMaxVersion = classId.Version;
   
}
function GetCalssIDError() {
    alert("Error Get Id");
}


//Error and Success function Add New Class
function SuccessfullyAddNewClass(data) {
    alert('Success Addidng class');
    ajaxCall("POST", "../api/section/addnewsection", JSON.stringify(generalSectionsArr), SuccessfullyAddNewSections, ErrorAddNewSections);

    //ajaxCall("POST", "../api/section/addnewsection", JSON.stringify(generalSectionsArr), SuccessfullyAddNewSections, ErrorAddNewSections);
}
function ErrorAddNewClass() {
    alert("Error Add New Class");
}
//Error and Success function Add New Sessions
function SuccessfullyAddNewSections(data) {
    alert("Successfully Add A new Sections");
}
function ErrorAddNewSections() {
    alert("Error Add A new Sections");
}


/***************************End Get Last Id And Save Class*****************************************************/

function AddClass(title, description) {
    
    Class = {
        Id: classMaxId + 1,
        Description: description,
        Title: title,
        Status:3,
        Position: -1,
        Score: 50,
        Version: classMaxVersion
        //Sections: generalSectionsArr
    }
    ajaxCall("POST", "../api/class/addnewclass", JSON.stringify(Class), SuccessfullyAddNewClass, ErrorAddNewClass);
}
//ajaxCall("POST", "../api/section/addnewsection", JSON.stringify(generalSectionsArr), SuccessfullyAddNewSections, ErrorAddNewSections);

//**********************************************************************************************************************
///Sorting and Show********************************************************************************************************'

function ShowSectionsFromDB() {

    class_object = JSON.parse(window.localStorage.getItem("classes"));
    generalSectionsArr = class_object.Sections;
    class_title = class_object.Title;
    class_desc = class_object.Description;

    document.getElementById("class-title").value = class_title;
    document.getElementById("class-desc").value = class_desc;

    CheckStatus();
    let status;
    let id;
    let title;
    var list;


    for (var i = 0; i < sNotApproved.length; i++) {
        status = sNotApproved[i].Status;
        id = sNotApproved[i].Id;
        title = sNotApproved[i].Title;
        list = document.getElementById(status);


        temp = list.innerHTML;
        temp = temp + "<li id="+ counter + " class='drag-item' style='position:relative;text-align:right'> " + title + "<img src='../Images/trash.png' onclick='Delete(this)' style='width:20px;height:20px;margin:5px;position:absolute;top:2px;left:1px' /></li > ";
        
        list.innerHTML = temp;

    }
    //Sorting the Approved classes
    sApprovedOrder = sApprovedOrder.sort((a, b) => (a.Position > b.Position) ? 1 : -1);

    for (var j = 0; j < sApprovedOrder.length; j++) {
        status = sApprovedOrder[j].Status;
        id = sApprovedOrder[j].Id;
        title = sApprovedOrder[j].Title;
        list = document.getElementById(status);
        temp = list.innerHTML;
        temp = temp + "<li id=" + id + " class='drag-item' style='position:relative;text-align:right'> " + title + "<img src='../Images/trash.png' onclick='Delete(this)' style='width:20px;height:20px;margin:5px;position:absolute;top:2px;left:1px' /></li > ";
        list.innerHTML = temp;
    }

}
//עדכון סטטוס ומיקום למקטעים חדשים
function UpdateSectionStatus() {
  
    var ct = document.getElementById("class-title").value;
    var cd = document.getElementById("class-desc").value;
    notReady = document.getElementById("2").children;
    waiting = document.getElementById("3").children;
    ready = document.getElementById("4").children;

    for (var i = 0; i < notReady.length; i++) {
        for (var j = 0; j < generalSectionsArr.length; j++) {
            if (generalSectionsArr[j].Id == notReady[i].id) {
                generalSectionsArr[j].Status = 2;
                generalSectionsArr[j].Position = -1;

            }

        }

    }
    for (var i = 0; i < waiting.length; i++) {
        for (var j = 0; j < generalSectionsArr.length; j++) {
            if (generalSectionsArr[j].Id == waiting[i].id) {
                generalSectionsArr[j].Status = 3;
                generalSectionsArr[j].Position = -1;
            }

        }
    }
    for (var i = 0; i < ready.length; i++) {
        for (var j = 0; j < generalSectionsArr.length; j++) {
            if (generalSectionsArr[j].Id == ready[i].id) {
                generalSectionsArr[j].Status = 4;
                generalSectionsArr[j].Position = i;
            }

        }
    }
    AddClass(ct, cd);
}
//בודק שקיימים סקשנים בשיעור החדש
function CheckExistingSections() {

    let notReady2 = document.getElementById("2").children;
    let waiting2 = document.getElementById("3").children;
    let ready2 = document.getElementById("4").children;
    if (notReady2.length == 0 && waiting2.length == 0 && ready2.length == 0) {
        alert("You have to add Section to class");
        return;
    }
    UpdateSectionStatus();
    }

function CheckStatus() {
    for (var i = 0; i < generalSectionsArr.length; i++) {


        switch (generalSectionsArr[i].Status) {
            case 2:
            case 3:
                sNotApproved.push(generalSectionsArr[i]);
                break;
            case 4:
                sApprovedOrder.push(generalSectionsArr[i]);
                break;
        }
    }
}