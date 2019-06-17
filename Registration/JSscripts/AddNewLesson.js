


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
var notReady;
var waiting;
var ready;

//Template for Section
function Section_Json(id, title, content, status, position, class_version, class_id, file_path) {
    this.Id = id,
        this.Title = title,
        this.Description = content,
        this.Status = status,
        this.Position = position,
        this.Version = class_version,
        this.ClassId = class_id,
        this.FilePath = file_path

}

//הוספת אובייקט ג'ייסון של מקטע חדש לתוך מערך המקטעים הכללי
function AddSection() {
    InsertImage();
    section_status = 2;
    var title = document.getElementById("section-title").value;
    var content = document.getElementById("section-content").value;
    var file_path = document.getElementById("section-image").value;
    var ready = document.getElementById("ready").checked;

    if (ready === true) {
        section_status = 3;
    }

    //Saving the section details in JSON object
    Section_Json.Id = counter;
    Section_Json.Description = content;
    Section_Json.Title = title;
    Section_Json.FilePath = file_path;
    Section_Json.Status = section_status;
    Section_Json.Position = -1;
    Section_Json.Version = classMaxVersion,
        Section_Json.ClassId = classMaxId + 1
    var sec = new Section_Json(Section_Json.Id, Section_Json.Title, Section_Json.Description, Section_Json.Status, Section_Json.Position, Section_Json.Version, Section_Json.ClassId, Section_Json.FilePath);

    var list = document.getElementById(section_status);
    temp = list.innerHTML;
    temp = temp + "<li id=" + counter + " class='drag-item' style='position:relative;text-align:right'> " + title + "<img src='../Images/trash.png' onclick='Delete(this," + counter + ") ' style='width: 20px; height: 20px; margin: 5px; position: absolute; top: 2px; left: 1px' /></li > ";
    list.innerHTML = temp;
    counter++;
    generalSectionsArr.push(sec);
    $("#squarespaceModal").modal("hide");
}

/***************************End Get Last Id And Save Class*****************************************************/

function AddClass(title, description, image, homeworkTitle, homeworkDesc, homeworkFile) {
    HomeWork = {
        Class_Id: classMaxId + 1,
        Class_Version: classMaxVersion,
        Homework_Name: homeworkTitle,
        Homework_Desc: homeworkDesc,
        Homework_Image: image,
        Homework_Audio: homeworkFile,
    }
    Class = {
        Id: classMaxId + 1,
        Description: description,
        Title: title,
        Status: 3,
        Position: -1,
        Score: 50,
        Version: classMaxVersion,
        Class_File_Path: image,
        HomeWork: HomeWork,
        //Sections: generalSectionsArr
    }
    ajaxCall("POST", "../api/class/addnewclass", JSON.stringify(Class), SuccessfullyAddNewClass, ErrorAddNewClass);
}


//עדכון סטטוס ומיקום למקטעים חדשים
function UpdateSectionStatus() {
    InsertImageToClass();
    InsertAudioToHomework();
    var ct = document.getElementById("class-title").value;
    var cd = document.getElementById("class-desc").value;
    var ci = document.getElementById("class-image").value;
    var classImageNameIndex = ci.lastIndexOf('\\');
    ci ="..\\Files"+ ci.substring(classImageNameIndex);

    var ht = document.getElementById("homework-title").value;
    var hd = document.getElementById("homework-desc").value;
    var hf = document.getElementById("homework-file").value;
    var homeworkImageNameIndex = hf.lastIndexOf('\\');
    hf = "..\\Files" + hf.substring(homeworkImageNameIndex);

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
    AddClass(ct, cd, ci,ht,hd,hf);
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

function Delete(e, id) {

    if (confirm("האם ברצונך למחוק את המקטע?")) {
        let indexToRemove;
        for (var i = 0; i < generalSectionsArr.length; i++) {
            if (id == generalSectionsArr[i].Id) {
                indexToRemove = i;
                break;
            }
        }
        if (indexToRemove > -1) {
            generalSectionsArr.splice(indexToRemove, 1);
        }
        e.parentNode.parentNode.removeChild(e.parentNode);
    }
}
//*************************************************************************************************************
//****************************All Ajax Success And Error Functions*********************************************

//ajaxCall("GET", "../api/class/getid", "", GetClassIDSuccess, GetCalssIDError);
//**************************************************************************** 
function GetClassIDSuccess(classId) {
    classMaxId = classId.Id;
    classMaxVersion = classId.Version;
}
function GetCalssIDError() {
    console.log("Error Get Id");
}

//ajaxCall("POST", "../api/class/addnewclass", JSON.stringify(Class), SuccessfullyAddNewClass, ErrorAddNewClass);
//*****************************************************************************************************
function SuccessfullyAddNewClass(data) {
    console.log('Success Addidng class');
    ajaxCall("POST", "../api/section/addnewsection", JSON.stringify(generalSectionsArr), SuccessfullyAddNewSections, ErrorAddNewSections);
}
function ErrorAddNewClass() {
    console.log("Error Add New Class");
}

//ajaxCall("POST", "../api/section/addnewsection", JSON.stringify(generalSectionsArr), SuccessfullyAddNewSections, ErrorAddNewSections);
//*****************************************************************************************************
function SuccessfullyAddNewSections(data) {
    console.log("Successfully Add A new Sections");
    if (confirm("האם את רוצה להוסיף עוד שיעורים?")) {
        location.reload();
    } else {
        window.location.href = 'ContentReview.html';
    }
}
function ErrorAddNewSections() {
    console.log("Error Add A new Sections");
}

