# Akiduki_Forest

## 概要
本ゲームは、尾花と桜Projectの第一作目として作成されました。<br />
こちらのMarkDownファイルやゲーム内の言語はすべて日本語表記となっています。<br />
ソースコードについては、余りにも汚いコード&組織機密なため、非公開とします。<br />

### 開発情報
- 開発体制
  - 開発元: LEC
  - 開発チーム: LEC-KISS
  - 開発メンバー: Lemon73
  - 開発コード: Akiduki_Forest

- 管理体制
  - 最新バージョン: 1.0.0
  - 対応OS: Windows / Linux
  - 対応言語: 日本語
  - ライセンス: 未定

- 技術
  - プログラム言語: C#
  - フレームワーク: .NET8.0(Console)
  - バージョン管理: GitHub / Git
  - IDE: Visual Studio
  - エディター: Visual Studio Code

## インストール方法
### Windows版
- 動作確認: Windows10 22H2
GitHub内のWindowsと書かれているファイルをインストール後、exeファイルを開く事でゲームを開始できます。<br />

または、あなたが好きなターミナル(Windows Terminalなど)で、.exeファイルのあるディレクトリに移動後、
```shell
./Akiduki_Forest.exe
```
にて起動ができます。

### MacOS版
LECではMacOSの環境がないため、ビルドができていません。<br />
そのため、MacOSでのプレイは不可能となっています。<br />

### Linux版
- 動作確認: WSL2 Debian (Windows10 22H2)
> [!NOTE]
> Linux実機での動作確認はしていません。

1. GitHub内のLinuxと書かれているファイルをインストール。
1. 起動(以下のコマンドを入力)
```shell
# cdコマンドでインストールしたLinuxファイルの場所まで移動
./Akiduki_Forest
```
> [!WARNING]
> (場合によってはchmodで権限を付与しないと動かない可能性もあります。)<br />
> (ターミナルで日本語が表示できる環境が整っていないと文字化けして読めません。)<br />

#### 開発者向け情報
Linux向けビルドコマンド
```shell
dotnet publish -r linux-x64 -c Release --self-contained true /p:PublishTrimmde=true /p:Publish
```
