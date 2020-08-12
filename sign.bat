zipalign -f -v 4 FlappyBird.apk FlappyBird-aligned.apk
del FlappyBird.apk
ren FlappyBird-aligned.apk FlappyBird.apk
jarsigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore C:/Users/Muhammad/release.keystore -storepass 123456 -keypass 123456 -signedjar FlappyBird-signed.apk FlappyBird.apk TicTacToe
del FlappyBird.apk
zipalign -f -v 4 FlappyBird-signed.apk FlappyBird.apk
del FlappyBird-signed.apk
pause