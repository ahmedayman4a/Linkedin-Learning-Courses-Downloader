[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/contains-tasty-spaghetti-code.svg)](https://forthebadge.com) [![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/N4N01KBWC)

# LinkedIn Learning Courses Downloader

> Download LinkedIn Learning courses in the video quality you like.

## Features

* Simple and easy to use GUI for Windows
* Download in the video quality you like (720p,  540p or 360p)
* Download Exercise files and subtitles automatically
* Download multiple courses at a time
* Automatically import LinkedIn Learning login token from Chrome, Firefox or Microsoft Edge
* Automatically detect the *enterpriseProfileHash* or the  *x-li-identity header* so all organization and library accounts should work

![Downloader Screenshot](https://raw.githubusercontent.com/ahmedayman4a/Linkedin-Learning-Courses-Downloader/d82584942ed880733edc9445910b7d457c19bb7f/LLCD.DownloaderGUI/img/Linkedin-Learning-Downloader-Screenshot.png)

## Easy install
Just go to the [releases section](https://github.com/ahmedayman4a/Linkedin-Learning-Courses-Downloader/releases), download the version that suits your platform and make sure you follow the requirements.

## Requirements
**Windows :** At least [.Net Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-web-installer). It already comes pre-installed with Windows 10 April 2018 Update (Version 1803) and later.

## How to use

- **Windows**

Just run the Setup file. A shortcut will be added to your desktop and start menu

* **Linux**

Open a terminal in the directory of the LinkedIn Learning Courses Downloader program then type :

```bash
  chmod 777 ./Linkedin Learning Courses Downloader
```

  

and to run the program type:

```bash
  ./Linkedin Learning Courses Downloader
```

## Getting the LinkedIn Learning login token cookie

#### You can now extract the token from your browser's default profile if you are logged into LinkedIn by pressing `Extract Token`. If it didn't work for you, manually get the token as follows (Make sure you are logged into LinkedIn Learning first):

* **Firefox**

1. Press `Shift+F9` on your keyboard **OR** right click anywhere on the LinkedIn Learning website , choose "Inspect Element" and click storage.
2. Look for the word "li_at" in the column "Name". Copy the value and paste it in the program.

![LinkedIn Firefox Token Tutorial GIF](https://raw.githubusercontent.com/ahmedayman4a/Linkedin-Learning-Courses-Downloader/main/LLCD.DownloaderGUI/img/LinkedinFirefoxTokenTutorial-min.gif)

* **Google Chrome**

1. Right click anywhere on the page and click inspect element **OR** press `F12` on your keyboard
2. Click on the 2 arrows in the top right corner beside the word performance then click Application
3. Double click on the word "Cookies" then click on https://www.linkedin.com
4. Look for the word "li_at" in the column "Name". Copy the value and paste it in the program.

![LinkedIn Chrome Token Tutorial](https://raw.githubusercontent.com/ahmedayman4a/Linkedin-Learning-Courses-Downloader/main/LLCD.DownloaderGUI/img/LinkedinChromeTokenTutorial.gif)

## How to build and run this code on your pc

You don't need to do that if you just want to run the app but if you want to build your own version here is how:

1. Open visual studio and click on file then Clone Repository.
2. For repository location type https://github.com/ahmedayman4a/Linkedin-Learning-Courses-Downloader.git.
3. Click Clone.
4. The code should be on your pc now. To edit the code, open the Linkedin-Learning-Courses-Downloader.sln file.
5. In order to run LLCD.DownloaderGUI you need to have Update.exe file inside the bin folder.
6. You can find this file under the LLCD.DownloaderGUI folder. Just copy and paste it in the bin folder.

## Contributions

I accept any contribution to the codebase whether it is a small bugfix or an exciting new feature as long as it works and fits the scope of the app. Just create a pull request and I will look into it as soon as I can.

## Buy me a coffee?

You can buy me a coffee using [PayPal(Kofi)](https://ko-fi.com/ahmedayman4a) or [Cryptocurrency](https://commerce.coinbase.com/checkout/be939297-c143-496f-a801-a7856ed9ac8b).

## Any Questions? Issues? Recommendations?

Just create an [issue](https://github.com/ahmedayman4a/Linkedin-Learning-Courses-Downloader/issues/new/choose) and I will reply as soon as I can.

## Acknowledgments

- Progress bar from [ShellProgressBar Project](https://github.com/Mpdreamz/shellprogressbar)
- Installer and Updater from [Squirrel](https://github.com/Squirrel/Squirrel.Windows)
