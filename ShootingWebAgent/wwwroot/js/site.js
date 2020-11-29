const data3 =
    '[' +
    '{' +
    '"Team":4,"TeamName":"SV B Team","FirstName":"Andreas","LastName":"Tappler","Count":1,"HR":104,"Range":1,"InternalCount":1,"DecValue":1,"DecValueSum":10.7,"InternalId":"1","SessionCount":4,' +
    '"Points":[{"x":0,"y":0},{"x":-100,"y":-100},{"x":-100,"y":-10},{"x":1000,"y":0}],' +
    '"Session":[{"value":10},{"value":20},{"value":30},{"value":40}]' +
    '},' +
    '{' +
    '"Team":4,"TeamName":"SV B Team","FirstName":"Franz","LastName":"Tappler","Count":5,"HR":114,"Range":2,"InternalCount":1,"DecValue":12,"DecValueSum":11.7,"InternalId":"2","SessionCount":4,' +
    '"Points":[{"x":0,"y":0}, {"x":-300,"y":100},{"x":0,"y":0}],' +
    '"Session":[{"value":40}]' +
    '},' +
    '{' +
    '"Team":4,"TeamName":"SV B Team","FirstName":"Franz","LastName":"dsadsas","Count":10,"HR":224,"Range":1,"InternalCount":1,"DecValue":13,"DecValueSum":12.7,"InternalId":"3","SessionCount":4,' +
    '"Points":[{"x":0,"y":0},{"x":-300,"y":100},{"x":0,"y":0}],' +
    '"Session":[{"value":55},{"value":22}]' +
    '},' +
    '{' +
    '"Team":4,"TeamName":"SV B Team","FirstName":"Noah","LastName":"Tappler","Count":12,"HR":404,"Range":2,"InternalCount":1,"DecValue":14,"DecValueSum":13.7,"InternalId":"4","SessionCount":4,' +
    '"Points":[{"x":0,"y":0}, {"x":-300,"y":100}, {"x":0,"y":0}],' +
    '"Session":[]' +
    '},' +
    '{' +
    '"Team":4,"TeamName":"SV B Team","FirstName":"Kerstin","LastName":"Kollegger","Count":13,"HR":204,"Range":3,"InternalCount":1,"DecValue":15,"DecValueSum":14.7,"InternalId":"5","SessionCount":4,' +
    '"Points":[{"x":0,"y":0}, {"x":100,"y":100},{"x":10,"y":10},{"x":-1000,"y":900}],' +
    '"Session":[{"value":100},{"value":200},{"value":300},{"value":400}]' +
    '}' +
    ']';

const dataRefresh =
    '[' +
    '{' +
    '"Team":3,"TeamName":"SV X Team","FirstName":"Andreas","LastName":"Tappler","Count":16,"HR":304,"Range":1,"InternalCount":1,"DecValue":5,"DecValueSum":8.7,"InternalId":"11","SessionCount":4,' +
    '"Points":[{"x":0,"y":0}],' +
    '"Session":[{"value":100},{"value":200},{"value":300},{"value":400}]' +
    '},' +
    '{' +
    '"Team":3,"TeamName":"SV X Team","FirstName":"Kerstin","LastName":"Kollegger","Count":5,"HR":114,"Range":3,"InternalCount":1,"DecValue":12,"DecValueSum":11.7,"InternalId":"15","SessionCount":4,' +
    '"Points":[],' +
    '"Session":[{"value":40}]' +
    '}' +
    ']';

var connection;
var table;

function check() {
    let parsedData = JSON.parse(data3);
    drawTargets(parsedData);
}

function check_one() {
    let parsedData = JSON.parse(dataRefresh);
    drawTargets(parsedData);
}

function showSecond() {
    setNavActive(1);
    $('#root').hide();
    $('#root-table').show();
}

function showFirst() {
    setNavActive(0);
    $('#root').show();
    $('#root-table').hide();
}

function setNavActive(id) {
    let nav = $('.nav.nav-tabs');
    nav.children().each(function () {
        $(this).children().first().removeClass("active");
    });

    nav.children().eq(id).children().first().addClass("active");
}

var App = (function () {

	let zoomMap = { 10: 0.17, 9: 0.11, 8: 0.09, 7: 0.07, 6: 0.06, 5: 0.05, 4: 0.04, 3: 0.04, 2: 0.04, 1: 0.04 };
	let colorMap = { 1: "red", 2: "green", 3: "yellow", 4: "navy", 5: "#c27e00", 6: "#00c2c2", 7: "#c200c2", 8: "#ff2e96", 9: "#c0955d", 10: "#8f8f8f" };
	let shotsColorMap = {10: "red", 9: "yellow", 8: "green", 7: "lightgreen", 6: "lightgreen", 5: "lightgreen", 4: "lightgreen", 3: "lightgreen", 2: "lightgreen", 1: "lightgreen", 0: "lightgreen" }

	var UpdateIndexPage = function (indexPageData) {
        console.log(indexPageData);
        let parsedData = JSON.parse(indexPageData);
        drawTargets(parsedData);
        
        table.updateOrAddData(indexPageData);
    };

    connection = new signalR.HubConnectionBuilder().withUrl("/updateHub").build();
    connection.on("UpdateIndexPage", UpdateIndexPage);

    //initialize table
    table = new Tabulator("#example-table", {
        layout: "fitColumns",      //fit columns to width of table
        responsiveLayout: "hide",  //hide columns that don't fit on the table
        tooltips: true,            //show tool tips on cells
        addRowPos: "top",          //when adding a new row, add it to the top of the table
        history: true,             //allow undo and redo actions on the table
        pagination: "local",       //paginate the data
        paginationSize: 7,         //allow 7 rows per page of data
        movableColumns: true,      //allow column order to be changed
        resizableRows: true,       //allow row order to be changed
        groupBy: "TeamName",
        initialSort: [             //set the initial sort order of the data
            { column: "Range", dir: "asc" },
        ],
        index: "StatisticModelId",
        columns: [                 //define the table columns
            { title: "Stand", field: "Range" },
            { title: "Vorname", field: "FirstName" },
            { title: "Nachname", field: "LastName" },
            { title: "Schüsse", field: "Count" },
            { title: "Summe", field: "DecValueSum"},
            { title: "HR", field: "HR" },
        ],
    });

    connection.start().then(() => {
	    initElements();
    });

    function initElements() {
        connection.invoke("InitData").catch(function (err) {
            return showError("Error","Can't establish connection to the server!");
        });
        showFirst();
    }

    drawTargets = function (parsedData) {
        // $('.target_container').children().remove();
        if (!parsedData.every((targetData) => {
            if (!isDataCorrect(targetData)) {
                showError("Error", "Got incorrect data, can't display values!");
                return false;
            }
            return true;
        })) return;

        parsedData.forEach(function (targetData) {
            if ($('.target_' + targetData.InternalId).length === 0)
                appendTarget(targetData)
            update_target(targetData);
            update_target_data(targetData);
        });

        sortTeams();
    }

    function appendTarget(targetData) {
        const team = $('.team_' + targetData.Team);
        if (team.length <= 0) {
            const newTeam =
                jQuery('<div class="container team_' + targetData.Team + '" style="max-width: 100%"></div>');

            const collapseMain = jQuery(collapseContainer);
            collapseMain.attr('data-sid', targetData.Team);
            $('.root').append(collapseMain);

            const collapsedButton = $('#collapseButtonTeam_');
            collapsedButton.attr('id', 'collapseButtonTeam_' + targetData.Team);
            collapsedButton.attr('data-target', '#collapseTeam_' + targetData.Team);
            collapsedButton.text(targetData.Team + ' - ' + targetData.TeamName);

            $('#collapseTeam_').attr('id', 'collapseTeam_' + targetData.Team);
            const collapseBody = $('#collapse_Team_');
            collapseBody.attr('id', 'collapse_Team_' + targetData.Team);
            collapseBody.append(newTeam);
        }

        const container = $('.container.team_' + targetData.Team);

        if (container.children().length <= 0) {
            const row = jQuery('<div class="row" style="padding-bottom: 10px; padding-top: 10px"></div>');
            row.append('<div class="target_container_' + targetData.Team + ' col align-self-start col-md-auto"></div>');
            container.append(row);
        } else {
            container.children().first()
                .append('<div class="target_container_' + targetData.Team + ' col align-self-start col-md-auto"></div>');
        }

        let target_container = $('.target_container_' + targetData.Team);

        target_container.each(function (index, element) {
            if (!element.hasChildNodes()) {
                target_container = jQuery(element);
                return false;
            }
        });

        let element = jQuery(target).get();
        target_container.append(element);
        $('.target_').attr('class', 'target_' + targetData.InternalId);
        $('.title_').attr('class', 'title_' + targetData.InternalId);
        $('.text-shots-counter_').attr('class', 'text-shots-counter text-shots-counter_' + targetData.InternalId);
        $('.current_shot_').attr('class', 'current_shot_' + targetData.InternalId);
        $('.current_sum_').attr('class', 'current_sum_' + targetData.InternalId);
        $('.current_hr_').attr('class', 'current_hr_' + targetData.InternalId);
        $('.team_color_').attr('class', 'team_color_' + targetData.InternalId);

        for (iter = 0; iter <= 6; iter++) {
            $('.session_h' + iter)
                .attr('class', 'col session session_h' + iter + '_' + targetData.InternalId + ' text-center');
            $('.session_' + iter + '_')
                .attr('class', 'session_' + iter + '_' + targetData.InternalId);
        }
    }

    function update_target (targetData) {
        let lowestShot = 10;
        const target = $('.target_' + targetData.InternalId);

        $('.circle_' + targetData.InternalId).remove();

        if (targetData.hasOwnProperty("Points")) {
            let counter = 1;
            for (let key in targetData.Points) {
                let t = document.createElementNS("http://www.w3.org/2000/svg", "circle");
                const mid = Math.sqrt(Math.pow(targetData.Points[key].x, 2) + Math.pow(targetData.Points[key].y, 2));
                let position = 10 - ((mid - 225) / 250);

                if (position < 1)
                    position = 1;
                position = Math.floor(position);

                t.setAttribute("r", 225);
                t.setAttribute("cx", targetData.Points[key].x);
                t.setAttribute("cy", targetData.Points[key].y);
                if (targetData.hasOwnProperty("InternalId"))
                    t.setAttribute("class", 'circle_' + targetData.InternalId);
                if (counter >= targetData.Points.length) {
                    t.setAttribute("fill", shotsColorMap[Math.floor(targetData.DecValue)]);
                } else {
                    t.setAttribute("fill", "rgba(128,128,128,0.75)");
                }
                target.append(t);
                counter++;

                const decValue = Math.floor(position);
                if (decValue < lowestShot)
                    lowestShot = decValue;
            }
            if (targetData.hasOwnProperty("DecValue") && counter > 1)
                zoomOut(zoomMap[lowestShot], target)
            else
                zoomOut(zoomMap[1], target)
            return;
        }
        zoomOut(zoomMap[1], target)
    }

    function update_target_data (targetData) {
        $('.text-shots-counter_' + targetData.InternalId).text(targetData.Count + "/" + targetData.ShotsCount);
        $('.title_' + targetData.InternalId).text(targetData.Range + " | "
            + targetData.FirstName + " " + targetData.LastName);
        $('.current_shot_' + targetData.InternalId).text(targetData.DecValue);
        $('.current_sum_' + targetData.InternalId).text(targetData.DecValueSum);
        $('.current_hr_' + targetData.InternalId).text(targetData.HR);
        $('.team_color_' + targetData.InternalId).attr('fill', colorMap[targetData.Team]);

        for (let iter = 1; iter <= 6; iter++) {
            if (targetData.SessionCount >= iter)
                $('.session_h' + iter + "_" + targetData.InternalId)
                    .attr('style', '');
            else
                $('.session_h' + iter + "_" + targetData.InternalId)
                    .attr('style', 'visibility: hidden;')

            if (targetData.Sessions.length >= iter) {
                $('.session_' + iter + '_' + targetData.InternalId)
                    .text(targetData.Sessions[iter - 1].value)
                    .attr('style', '');
            } else
                $('.session_' + iter + '_' + targetData.InternalId)
                    .attr('style', 'visibility: hidden;')
        }
    }

    function zoomOut (zoomLevel, target) {
        target.attr("transform", "matrix(" + zoomLevel + ",0,0," + zoomLevel + ",0,0)")
    }

    function sortTeams() {
        const root = $('.root'), container = root.children('div');
        container.detach().sort(sortBySrtId);
        root.append(container);
    }

    function showError(errorTitle, errorMessage) {
        $(".modal-title").text(errorTitle);
        $(".modal-body").text(errorMessage);
        $("#myModal").modal('show');
    }

    function isDataCorrect(targetData) {
        if (!targetData.hasOwnProperty("Team"))
            return false;
        if (!targetData.hasOwnProperty("TeamName"))
            return false;
        if (!targetData.hasOwnProperty("Range"))
            return false;
        if (!targetData.hasOwnProperty("InternalId"))
            return false;
        if (!targetData.hasOwnProperty("FirstName"))
            return false;
        if (!targetData.hasOwnProperty("LastName"))
            return false;
        if (!targetData.hasOwnProperty("Count"))
            return false;
        if (!targetData.hasOwnProperty("InternalCount"))
            return false;

        if (targetData.Count !== targetData.Count) {
            return false;
        }

        if (!targetData.hasOwnProperty("HR"))
            return false;
        if (!targetData.hasOwnProperty("DecValue"))
            return false;
        if (!targetData.hasOwnProperty("DecValueSum"))
            return false;
        if (!targetData.hasOwnProperty("Points"))
            return false;
        if (!targetData.hasOwnProperty("Sessions"))
            return false;
        if (!targetData.hasOwnProperty("SessionCount"))
            return false;
        if (!targetData.hasOwnProperty("ShotsCount"))
            return false;

        return true;
    }

    function sortBySrtId(a, b) {
        const astts = $(a).data('sid');
        const bstts = $(b).data('sid');
        return (astts > bstts) ? (astts > bstts) ? 1 : 0 : -1;
    }
}());