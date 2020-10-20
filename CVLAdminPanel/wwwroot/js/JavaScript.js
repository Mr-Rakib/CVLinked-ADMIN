function OpenHeader() {
    document.getElementById("headernav").classList.toggle("active");
}
function Unavailable() {
    alert('Service Not Available');
}
function ActiveConfirmation() {
    return confirm('Are you sure to active this user ?');
}

function DeactiveConfirmation() {
    return confirm('Are you sure to deactive this user ?');
}

function DeleteConfirmation() {
    return confirm("Are you sure to delete?");
}

function VerificationConfirmation() {
    return confirm('Are you sure to verify..?');
}

function ApproveConfirmation() {
    return confirm('Are you sure to Approve..?');
}

function NotApproveConfirmation() {
    alert('Service Not Available');
    swal("Please enter OrderCode !");
}

function JsonDescriptionView(id) {
    var Id = 'change-log-json-value-' + id;
    var value = document.getElementById(Id).style.display;
    if (value == "none" || value == "" ) {
        document.getElementById(Id).style.display = "block";
    }
    else {
        document.getElementById(Id).style.display = "none";
    }
}

$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

$('#view-error-modal').on('show.bs.modal',
    function (e) {
        var Id = $(e.relatedTarget).attr('data-id');
        $('#viewerrormodal').load('/Log/ErrorLog?errorId=' + Id);
    }
);

var loadFile = function (event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};

function getFilePath() {
    $('input[type=file]').change(
        function () {
            var filePath = $('#fileUpload').val();
            return filePath;
        }
    );
}

$("#changeBtn").click(function () {
    $("#darkMode").toggle("darkMode");
});

function updateStyleSheet() {
    var x = ("#dark-css-open").attr("href");
    $('#dark-css-open').attr("href", "/css/darkmode.css");
    $("#main-logo").attr('src', '/Content/img/logo-white.png');
}


function TogglePassword() {
    var x = document.getElementById("show_hide_password");
    if (x.type == "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function ChangeTheme() {
    var x = localStorage.getItem("theme");
    if (localStorage.getItem("theme") == "" || localStorage.getItem("theme") == "Light") {
        localStorage.setItem("theme", "Dark");
        $('#dark-mode-css').attr('href', "/css/darkmode.css");
        $('#main-logo').attr('src', '/Content/img/logo-white.png');
        $('#theme-changer').html("<i class='fas fa-retweet'></i> Light Mode");
    }
    else {
        $('#dark-mode-css').attr('href', "");
        localStorage.setItem("theme", "Light");
        $('#main-logo').attr('src', '/Content/img/brand/logo.png');
        $('#theme-changer').html("<i class='fas fa-retweet'></i> Dark Mode");
    }
}
