function setupTable(tableId, selectId) {
    var htmlBody = document.getElementById(tableId),
    htmlSelect = document.getElementById(selectId),
    rowTpl = document.createElement("tr");

    htmlSelect.onchange = function (e) {
        var fields = htmlSelect.options[htmlSelect.selectedIndex].value;
        while (rowTpl.firstChild) rowTpl.removeChild(rowTpl.firstChild);
        for (var i = 0; i < fields; i++) {
            var td = document.createElement("td");
            input = document.createElement("input");
            input.name = i;
            input.size = 5;
            td.appendChild(input);
            rowTpl.appendChild(td);
        }


        var numRows = htmlSelect.options[htmlSelect.selectedIndex].value;
        while (htmlBody.firstChild) htmlBody.removeChild(htmlBody.firstChild);
        for (var i = 0; i < numRows; i++) {
            htmlBody.appendChild(rowTpl.cloneNode(true));
        }
    }
}