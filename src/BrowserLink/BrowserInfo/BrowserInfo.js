(function (browserLink, $) {
    /// <param name="browserLink" value="bl" />
    /// <param name="$" value="jQuery" />

    function SendInfo() {

        var width = window.innerWidth || document.body.clientWidth;
        var height = window.innerHeight || document.body.clientHeight;

        browserLink.invoke("CollectInfo", width, height);
    }

    window.addEventListener('resize', function (event) {
        SendInfo();
    });

    return {

        onConnected: function () { // Optional. Is called when a connection is established
            SendInfo();
        }
    };
});