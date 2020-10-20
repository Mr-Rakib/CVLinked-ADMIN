
$(document).ready(function () {
    var x = $("#course_id");
    var z = $("#semester_id");

    var y = $("#evaluation_id");

    y.prop('disabled', true);

    z.change(function () {

        if ($(this).val() == "0" && x.val() == "0") {
            y.prop('disabled', true);
            y.val("0");
        }
        else {
            $.ajax({
                url: "/Extra/FindEvaluationByCourseId/" + x.val() + "/" + z.val(),
                nethod: "get",
                success: function (data) {
                    y.prop('disabled', false);
                    y.empty();
                    y.append($('<option/>', { value: '0', text: "Select-Semester-Marks" }));
                    $(data).each(
                        function (index, item) {
                            y.append($('<option/>', { value: item.id, text: item.assessment.name + "-[Semester: " + item.semesterId + "]-(" + item.marks + ")" }));
                        });
                }
            });
        }

    });
});

$(document).ready(function () {
    var x = $("#evaluation_id");
    var y = $("#clo_id");
    y.prop('disabled', true);

    x.change(function () {
        if ($(this).val() == "0") {
            y.prop('disabled', true);
            y.val("0");
        } else {

            $.ajax({
                url: "/Extra/FindByEvaluationId/" + x.val(),
                nethod: "get",
                success: function (data) {
                    y.prop('disabled', false);
                    y.empty();
                    y.append($('<option/>', { value: '0', text: "Select" }));
                    $(data).each(
                        function (index, item) {
                            y.append($('<option/>', { value: item.id, text: item.name }));
                        });
                }
            });
        }
    });
});





$(document).ready(function () {

    $('#individual_search_table tfoot th').each(function () {

        var title = $(this).text();
        if (title != '')
            $(this).html('<input type="text" class="form-control form-control-sm custom-search-box" style = "width: 100%" placeholder="Search ' + title + '" />');

    });

    // DataTable
    var table = $('#individual_search_table').DataTable({
        initComplete: function () {
            this.api().columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });

});


$(document).ready(function () {

    $('#error_log_table tfoot th').each(function () {

        var title = $(this).text();
        if (title != 'Error Time')
            $(this).html('<input type="text" class="form-control form-control-sm custom-search-box" style = "width: 100%" placeholder="Search ' + title + '" />');

    });

    // DataTable
    var table = $('#error_log_table').DataTable({
        "lengthMenu": [[25, 50, 100, -1], [25, 50, 100, "All"]],
        "pageLength": 50,
        initComplete: function () {
            this.api().columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });

});



$(document).ready(function () {

    $('#employer_table tfoot th').each(function () {

        var title = $(this).text();
        if (title != '')
            $(this).html('<input type="text" class="form-control form-control-sm custom-search-box" style = "width: 90%;margin: 0 5% 0 5%;" placeholder="Search ' + title + '" />');
        else
            $(this).html('<i class="fas fa-arrow-alt-circle-left text-primary"></i> <i class="fas fa-arrow-alt-circle-down text-primary"></i>');

    });

    // DataTable
    var table = $('#employer_table').DataTable({
        "order": [[3, "desc"]], //or asc 
        initComplete: function () {
            this.api().columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });

});


$(document).ready(function () {

    $('#package_history_table tfoot th').each(function () {

        var title = $(this).text();

        if (title != '')
            $(this).html('<input type="text" class="form-control form-control-sm custom-search-box" style = "width: 90%;margin: 0 5% 0 5%;" placeholder="Search ' + title + '" />');

    });


    // DataTable
    var table = $('#package_history_table').DataTable({
        initComplete: function () {
            this.api().columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });

});



$(document).ready(function () {

    $('#request_log_table tfoot th').each(function () {
        var title = $(this).text().trim();

        if (title == 'Response Status')
            $(this).html
                (
                    '<select class="form-control form-control-sm">' +
                    '<option value="OK">Select Status</option>' +
                    '<option value="OK">OK</option>' +
                    '<option value="Unauthorized">Unauthorized</option>' +
                    '<option value="BadRequest">Bad Request</option>' +
                    '<option value="MethodNotAllowed">Method Not Allowed</option>' +
                    '</select >'
                );

        else if (title == "Client's IP")
            $(this).html('<input type="text" id="clientip" class="form-control form-control-sm custom-search-box " style = "width: 100%;" placeholder="Search ' + title + '"/>');

        else if (title == "Instance")
            $(this).html('<input type="text" id= "request-instance" class="form-control form-control-sm custom-search-box " style = "width: 100%;" placeholder="Search ' + title + '"/>');
    });

    // DataTable
    var table = $('#request_log_table').DataTable({
        "lengthMenu": [[25, 50, 100, -1], [25, 50, 100, "All"]],
        "pageLength": 50,
        initComplete: function () {
            this.api().columns().every(function () {
                var that = this;
               
                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });

                $('select', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });

});

function AppendRequestInstance(data){

    document.getElementById('request-instance').value = data;
}

$(document).on("click", ".ClickToEdit", function () {
    var custID = $(this).data("customerid");
    window.opener.location.href("/Edit/Edit?id=" + custID);
});