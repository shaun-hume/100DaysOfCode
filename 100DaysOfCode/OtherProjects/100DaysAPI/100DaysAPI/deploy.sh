echo "---------- Removing old 100Days API ----------"
docker ps -a | awk '{ print $1,$2 }' | grep 100daysapi | awk '{print $1 }' | xargs -I {} docker stop rm {}

echo "---------- Building 100days API ----------"
docker build -t 100daysapi .
echo "---------- Running 100days API ----------"
docker run -d --rm -it -p 5000:5000 100daysapi
