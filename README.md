# Akiduki_Forest

## 概要
本ゲームは、尾花と桜Projectの第一作目として作成されました。<br />
こちらのMarkDownファイルやゲーム内の言語はすべて日本語表記となっています。<br />
ソースコードについては、余りにも汚いコード&組織機密なため、非公開とします。<br />

### 開発情報
開発元:LEC<br />
開発チーム:LEC-KISS<br />
開発メンバー:Lemon73<br />
開発コード:Akiduki_Forest<br />

最新バージョン:1.0.0.0<br />
対応OS:Windows/Linux<br />
対応言語:日本語<br />
ライセンス:未定<br />

プログラム言語:C#<br />
フレームワーク:.NET7.0(Console)[Microsoft]<br />
IDE:Visual Studio[Microsoft]<br />
エディター: Visual Studio Code[Microsoft]<br />

## インストール方法
### Windows版
```
動作確認…Windows10 22H2
```
GitHub内のWindowsと書かれているファイルをインストール後、exeファイルを開く事でゲームを開始できます。<br />

または、あなたが好きなターミナル(Windows Terminalなど)で、.exeファイルのあるディレクトリに移動後、
```
./Akiduki_Forest.exe
```
にて起動ができます。

### MacOS版
LECではMacOSの環境がないため、ビルドができていません。<br />
そのため、MacOSでのプレイは不可能となっています。<br />

### Linux版
```
動作確認…Windows10 22H2 | WSL2 Debian
```
(Linux実機での動作確認はしていません。)<br />

GitHub内のLinuxと書かれているファイルをインストール。

(起動)
```
(cdコマンドでインストールしたLinuxファイルの場所まで移動)
./Akiduki_Forest
```
(場合によってはchmodで権限を付与しないと動かない可能性もあります。)<br />
(ターミナルで日本語が表示できる環境が整っていないと文字化けして読めません。)<br />

#### 開発者向け情報
Linux向けビルドコマンド…
```
dotnet publish -r linux-x64 -c Release --self-contained true /p:PublishTrimmde=true /p:Publish
```
