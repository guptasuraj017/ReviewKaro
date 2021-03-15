
    $(document).ready(function () {


        $("#pending").prop("checked", true);
        $("#submitted").prop("checked", true);
        $.ajax({
        url: "/Employee/AssignedReviewsToSubmit",
            method: "get",
            dataType: "html",
            success: function (response) {
        $("#pendingDiv").empty();
                $("#pendingDiv").append(response);

            }

        });
        $.ajax({
        url: "/Employee/SubmittedReviewsByReviewer",
            method: "get",
            dataType: "html",
            success: function (response) {
        $("#submittedDiv").empty();
                $("#submittedDiv").append(response);

            }

        });
    });

    $("#pending").click(function () {
        if ($(this).prop("checked")) {
        
            $.ajax({
        url: "/Employee/AssignedReviewsToSubmit",
                method: "get",
                dataType: "html",
                success: function (response) {
        $("#pendingDiv").empty();
                    $("#pendingDiv").append(response);

                }

            });
        }
        else {
        $("#pendingDiv").empty();
        }
    });
    $("#submitted").click(function () {

        if ($(this).prop("checked")) {
       
            $.ajax({
        url: "/Employee/SubmittedReviewsByReviewer",
                method: "get",
                dataType: "html",
                success: function (response) {
        $("#submittedDiv").empty();
                    $("#submittedDiv").append(response);

                }

            });
        }
        else {
        $("#submittedDiv").empty();
        }
    });

