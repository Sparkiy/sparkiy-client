git add -A
git commit -m "Before merge commit"
git push

git checkout development
git pull
git merge edge
git push

git checkout beta
git pull
git merge development
git push

git checkout master
git pull
git merge beta
git push

git checkout beta
git merge master
git push

git checkout development
git merge beta
git push

git checkout edge
git merge development
git push