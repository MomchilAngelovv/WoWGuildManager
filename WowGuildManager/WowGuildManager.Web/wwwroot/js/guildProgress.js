function addProgress(event) {
    let raidName = event.target.value;
    $.get({
        url: 'https://localhost:44309/Guild/AddProgress?raidName=' + raidName,
        success: function (data) {
            document.documentElement.innerHTML = data;
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function removeProgress(event) {
    let raidName = event.target.value;
    $.get({
        url: 'https://localhost:44309/Guild/RemoveProgress?raidName=' + raidName,
        success: function (data) {
            document.documentElement.innerHTML = data;
        },
        error: function (error) {
            console.log(error);
        }
    });
}   