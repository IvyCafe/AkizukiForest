# 秋月と森

## 概要
本ゲームは、 Ivy Cafeteria の第一作目として作成されました。  
こちらの MarkDown ファイル及びゲーム内の言語はすべて日本語表記となっています。  
ソースコードについては、余りにも汚いコード&組織機密なため、非公開とします。

### 開発情報
- 開発体制
  - 開発元: Ivy Cafeteria
  - 開発チーム: 秋月と森 開発委員会
  - 開発メンバー: Lemon73
  - 開発コード: AkizukiForest

- 管理体制
  - 最新バージョン: 1.0.0
  - 対応OS: Windows / Linux
  - 対応言語: 日本語
  - ライセンス: 未定

- 技術
  - プログラム言語: C#
  - フレームワーク: .NET 8.0 (Console)
  - バージョン管理: Git
  - IDE: Visual Studio
  - エディター: Visual Studio Code

## インストール方法
### Windows
- 動作確認: Windows10 22H2
GitHub 内の Windows と書かれているファイルをインストール後、 `.exe` ファイルを開く事でゲームを開始できます。

または、あなたが好きなターミナル (Windows Terminal など) で、 `.exe` ファイルのあるディレクトリに移動後、
```shell
./Akiduki_Forest.exe
```
にて起動ができます。

### MacOS
Lemon's Resting Area では MacOS の環境がないため、ビルドができていません。  
そのため、 MacOS でのプレイは不可能となっています。

### Linux
- 動作確認: WSL2 Debian (Windows10 22H2)
> [!NOTE]
> Linux 実機での動作確認はしていません。

1. GitHub 内の Linux と書かれているファイルをインストール。
1. 起動 (以下のコマンドを入力)
```shell
# cd コマンドでインストールした Linux ファイルの場所まで移動
./Akiduki_Forest
```
> [!WARNING]
> (場合によっては `chmod` で権限を付与しないと動かない可能性もあります。)  
> (ターミナルで日本語が表示できる環境が整っていないと文字化けして読めません。)

#### 開発者向け情報
Linux 向けビルドコマンド
```shell
dotnet publish -r linux-x64 -c Release --self-contained true /p:PublishTrimmde=true /p:Publish
```
