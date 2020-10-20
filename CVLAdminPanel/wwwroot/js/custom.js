$(document).ready(function () {

    // --------------------------------------Configure thr theme----------------------------------------------------
    if (localStorage.getItem("theme") == "Dark") {
        document.getElementById('main-logo').setAttribute('src', '/Content/img/logo-white.png');
        document.getElementById('dark-mode-css').setAttribute('href', "/css/darkmode.css");
        document.getElementById('theme-changer').innerHTML = "<i class='fas fa-retweet'></i> Light Mode";
    }
    else {
        document.getElementById('main-logo').setAttribute('src', '/Content/img/brand/logo.png');
        document.getElementById('dark-mode-css').setAttribute('href', "");
        document.getElementById('theme-changer').innerHTML = "<i class='fas fa-retweet'></i> Dark Mode";
    }

    //----------------------------------Packages--------------------------------//
    $('#add-package-modal').on('show.bs.modal',
        function (e) {
            $('#packageSaveModal').load('/Packages/SavePackageView');
        });

    $('#edit-package-modal').on('show.bs.modal',
        function (e) {
            var packageId = $(e.relatedTarget).attr('data-id');
            $('#packageUpdModal').load('/Packages/GetPackagesById?id=' + packageId);
        });

    $('#delete-package-modal').on('show.bs.modal',
        function (e) {
            var packageId = $(e.relatedTarget).attr('data-id');
            $('#packageDeleteModal').load('/Packages/DeletePackagesById?id=' + packageId);
        });


    $('#delete-modal').on('show.bs.modal',
        function (e) {
            var promotionId = $(e.relatedTarget).attr('data-id');
            $('#promotionDeleteModal').load('/PromotionalOffer/DeletePromotionById?promotionId=' + promotionId);
        });

    //Promotional Offer 

    $('#company-email-modal').on('show.bs.modal',
        function (e) {
            var employeeId = $(e.relatedTarget).attr('data-id');
            $('#companyEmailModal').load('/Company/SendEmailView?id=' + employeeId);
        });

    $('#sms-modal').on('show.bs.modal',
        function (e) {
            var employeeId = $(e.relatedTarget).attr('data-id');
            $('#companySmsModal').load('/Company/SendSmsView?id=' + employeeId);
        });

    $('#add-promotiona-modal').on('show.bs.modal',
        function () {
            $('#promotioneAddModal').load('/PromotionalOffer/PromotionAddView');
        }
    );


    $('#view-details-modal').on('show.bs.modal',
        function (e) {
            var employeeId = $(e.relatedTarget).attr('data-id');
            $('#viewdetailsmodal').load('/Company/DetailsGrid?id=' + employeeId);
        }
    );


    $('#view-details-modal').on('show.bs.modal',
        function (e) {
            var employeeId = $(e.relatedTarget).attr('data-id');
            $('#viewdetailsmodal').load('/Company/DetailsGrid?id=' + employeeId);
        }
    );
    //-------------------------------Back Office User-------------------------------//
    $('#details-backoffice-user').on('hidden.bs.modal').on('show.bs.modal',
        function (e) {
            var loginId = $(e.relatedTarget).attr('data-id');
            $('#backofficeuserdetails').load('/BackOfficeUser/Details?loginId=' + loginId);
        }
    );
});

