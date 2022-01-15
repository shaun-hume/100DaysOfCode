######INSTALL DOCKER######
curl -sSL https://get.docker.com | sh
sudo groupadd docker
sudo usermod -aG docker <your username to add to docker group here>
logout


######INSTALL PORTAINER######
docker volume create portainer_data
docker run -d -p 8000:8000 -p 9000:9000 --name=portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer


######INSTALL GIT######
sudo apt-get install git
git config --global user.name "YOUR_NAME_HERE"
git config --global user.email "YOUR_EMAIL_HERE"
git config -l


######CLONE REPO######
mkdir ~/src/
cd ~/src
git config credential.helper store
git clone {Github repo}

