<?php
// data
$file = "/home/vol3_2/infinityfree.com/if0_37477620/files/NetWare.zip";
$token = "22e222266e941c8ce149f7234675d7210d085f5731fc2bdcb50acda209b996e5";

// style
echo "<body style='background-color: black; color: white;'>";

// check referer
if (!isset($_SERVER["HTTP_REFERER"]) || strpos($_SERVER["HTTP_REFERER"], "krustykrack.wuaze.com/index.html") === false) {
    die("ERROR 000B : Attempt to access download page directly or page refresh detected.");
}

// check hash
$hash = isset($_GET["hash"]) ? $_GET["hash"] : "";
if (!$hash) {
    die("ERROR 001B : Hash not found.");
}

// create post data and curl session
$postData = array(
    "token" => $token,
    "hash" => $hash
);
$curlSession = curl_init("https://publisher.linkvertise.com/api/v1/anti_bypassing");

// setup curl options
curl_setopt($curlSession, CURLOPT_RETURNTRANSFER, true);
curl_setopt($curlSession, CURLOPT_POST, true);
curl_setopt($curlSession, CURLOPT_POSTFIELDS, http_build_query($postData));

// execute request
$response = curl_exec($curlSession);
if (curl_errno($curlSession)) {
    die("ERROR 002B : Could not reach LinkVertise API.");
}
curl_close($curlSession);

// decode response
$responseData = json_decode($response, true);

// check the api response
if (isset($responseData["status"]) && $responseData["status"]) {
    if (file_exists($file)) {
        ob_clean();

        // get file handle
        $fileHandle = fopen($file, 'rb');
        if ($fileHandle === false) {
            die("ERROR 003B : Could not open download file.");
        }

        // set download headers
        header("Content-Description: File Transfer");
        header("Content-Type: application/zip");
        header('Content-Disposition: attachment; filename="' . basename($file) . '"');
        header("Expires: 0");
        header("Cache-Control: must-revalidate");
        header("Pragma: public");
        header("Content-Length: " . filesize($file));
        
        // download file
        while (!feof($fileHandle)) {
            echo fread($fileHandle, 8192);
            flush();
        }
        exit;
    } else {
        die("ERROR 004B : NetWare file not found.");
    }
} else {
    die("ERROR 005B : Invalid LinkVertise hash, repeat LinkVertise process.");
}
?>
