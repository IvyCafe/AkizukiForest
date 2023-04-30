# Akiduki_Forest

## 概要

本ゲームは、尾花と桜Projectの第一作目として作成されました。<br>
完全日本人向けに開発しているため、こちらのMarkDownファイルやゲーム内の言語はすべて日本語です。<br>
ソースコードについては、余りにも汚いコード&組織機密なため、非公開とします。<br>

#### 開発情報

		開発元:LEC
		開発チーム:LEC-KISS
		開発メンバー:Lemon73
		開発コード:Akiduki_Forest

		最新バージョン:1.0.0.0
		対応OS:Windows/Linux
		対応言語:日本語
		ライセンス:未定

		プログラム言語:C#
		フレームワーク:.NET7.0(コンソールアプリケーション)[Microsoft]
		IDE:Visual Studio[Microsoft]

## インストール方法

#### Windows版

		動作確認…Windows10 22H2

GitHub内のWindowsと書かれているファイルをインストール後、exeファイルを開く事でゲームを開始できます。<br>
.NET7.0がインストールされていない場合は動作しませんので、Microsoft公式からインストールしてください。<br>
(おそらく最新版のWindowsなら既にインストールされていると思います。)<br>

#### MacOS版

LECではMacOSの環境がないため、ビルドができていません。<br>
そのため、MacOSでのプレイは不可能となっています。<br>

#### Linux版

		動作確認…Windows10 22H2内のWSL2 Debian版

なお、Linux実機での動作確認はしていません。<br>

Linuxでは既存の状態では.NET7.0がインストールされていないので、そちらのインストールを先に行います。

(以下はすでにインストールされている可能性があります)

		sudo apt-get install -y apt-transport-https

(.NET7.0)

		sudo apt-get install -y dotnet-sdk-7.0

(更新)

		sudo apt-get update

GitHub内のLinuxと書かれているファイルをインストール。

(起動)

		(cdコマンドでインストールしたLinuxファイルの場所まで移動)
		./Akiduki_Forest

(場合によってはchmodで権限を付与しないと動かない可能性もあります。)

## 開発者用
ビルド設定…

		dotnet publish -r linux-x64 -c Release --self-contained true /p:PublishTrimmde=true /p:Publish
