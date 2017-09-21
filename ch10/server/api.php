<?php 

$message = $_POST['message'];
$cloudiness = $_POST['cloud_value'];
$timestamp = $_POST['timestamp'];
$combined = $message." cloudiness=".$cloudiness." time=".$timestamp."\n";

$filename = "data.txt";
file_put_contents($filename, $combined, FILE_APPEND | LOCK_EX);

echo "Logged";

?>