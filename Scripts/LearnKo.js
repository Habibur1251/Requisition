/// <reference path="jquery-2.0.3.min.js" />
/// <reference path="knockout-3.0.0.js" />



function Student(data) {
    this.StudentId = ko.observable(data.StudentId);
    this.FirstName = ko.observable(data.FirstName);
    this.LastName = ko.observable(data.LastName);
    this.Age = ko.observable(data.Age);
    this.Gender = ko.observable(data.Gender);
    this.Batch = ko.observable(data.Batch);
    this.Address = ko.observable(data.Address);
    this.Class = ko.observable(data.Class);
    this.School = ko.observable(data.School);
    this.Domicile = ko.observable(data.Domicile);

}


function StudentViewModel() {
    var self = this;
    self.Domiciles = ko.observableArray(['Delhi', 'Outside Delhi']);
    self.Genders = ko.observableArray(['Male', 'Female']);
    self.Students = ko.observableArray([]);
    self.StudentId = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.Age = ko.observable();
    self.Batch = ko.observable();
    self.Address = ko.observable();
    self.Class = ko.observable();
    self.School = ko.observable();
    self.Domicile = ko.observable();
    self.Gender = ko.observable();


    self.AddStudent = function () {
        self.Students.push(new Student({
            StudentId: self.StudentId(),
            FirstName: self.FirstName(),
            LastName: self.LastName(),
            Domicile: self.Domicile(),
            Age: self.Age(),
            Batch: self.Batch(),
            Address: self.Address(),
            Class: self.Class(),
            School: self.School(),
            Gender: self.Gender()
        }));
        self.StudentId(""),
        self.FirstName(""),
        self.LastName(""),
        self.Domicile(""),
        self.Age(""),
        self.Batch(""),
        self.Address(""),
        self.Class(""),
        self.School(""),
       self.Gender("")
    };

    self.DeleteStudent = function (student) {

        $.ajax({
            type: "POST",
            url: 'LearnKO.aspx/DeleteStudent',
            data: ko.toJSON({ data: student }),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                alert(result.d);
                self.Students.remove(student)
            },
            error: function (err) {
                alert(err.status + " - " + err.statusText);
            }
        });

    };

    self.SaveStudent = function () {
        $.ajax({
            type: "POST",
            url: 'LearnKO.aspx/SaveStudent',
            data: ko.toJSON({ data: self.Students }),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                alert(result.d);
            },
            error: function (err) {
                alert(err.status + " - " + err.statusText);
            }
        });
    };

    $.ajax({
        type: "POST",
        url: 'LearnKO.aspx/FetchStudents',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (results) {
            var students = $.map(results.d, function (item) {
                return new Student(item)
            });
            self.Students(students);
        },
        error: function (err) {
            alert(err.status + " - " + err.statusText);
        }
    })
}

$(document).ready(function () {
    ko.applyBindings(new StudentViewModel());
});
