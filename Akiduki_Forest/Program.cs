﻿// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Text;
using System.Xml.Linq;

bool playing = true;
//Null警告を出さないためにstring"?"としている。
string? command;

//セーブ用の区切り(節)
int section = 0;

//ステータス
//int hp = 200;//体力
int lack = 0;//運

//アイテム
int item = 0;//アイテムの総重量
int item_bom = 0;//手榴弾の数(手榴弾の重さは*2で算出)
int item_bullet = 0;//弾丸の数
int item_cure = 0;//医療品の数

//誤ったコマンドを入力しているときはループ
while (playing == true)
{
    Console.WriteLine("1:はじめから");
    Console.WriteLine("2:途中から");
    Console.WriteLine("3:終了する");
    Console.Write("数値を入力してください:");
    command = Console.ReadLine();
    Console.WriteLine("");
    switch (command) 
    {
        case "1":
            playing = false;
            Console.WriteLine("「うーん…暇だなぁ」");
            Console.ReadLine();//次のコメントを表示(ReadKeyでもいいけど、ReadLineのほうが改行されて読みやすい気がするのでこちらに。)
            Console.WriteLine("彼女、秋月は暇をしていた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("数年前の大規模な戦争も今は終わり、平和な世の中となった今では傭兵としての仕事はなくなっていた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("ここ、アネリアの森は都市から離れた場所にあり、秋月が住んでいる家はその森の中にあった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「とりあえず、外で散歩でもしようかなぁ…」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("秋月はそう言って椅子から立ち上がった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("近くの机に置いている拳銃と、2本の小型ナイフを持ち、近くのリュックを背負った。");
            Console.ReadLine();//次のコメントを表示

            //初期状態
            item = 12;//アイテムの総重量
            item_bom = 0;//手榴弾の数(手榴弾の重さは*2で算出)
            item_bullet = 0;//弾丸の数
            item_cure = 0;//医療品の数

            //アイテム選択処理
            bool choose_item = true;
            while (choose_item == true) 
            {
                Console.WriteLine("");
                Console.WriteLine("その他に冒険に持っていくものを決めてください");
                Console.WriteLine("");
                Console.WriteLine("持っていけるもの(最大12)");
                Console.WriteLine("1:手榴弾(攻撃/爆破)[1つ当たりの重さ:2]");
                Console.WriteLine("2:弾薬(攻撃)[1つ当たりの重さ:1]");
                Console.WriteLine("3:医療品(回復)[1つ当たりの重さ:1]");
                Console.WriteLine("");

                bool choose_item_bom = true;
                while (choose_item_bom == true)
                {
                    Console.WriteLine("1:手榴弾 を何個持っていきますか?");
                    if (!int.TryParse(Console.ReadLine(), out item_bom))
                    {
                        Console.WriteLine("半角数値を入力してください");
                    }
                    else
                    {
                        if (item - item_bom * 2 >= 0) 
                        {
                            item -= item_bom * 2;
                            Console.WriteLine("{0}個(重さ{1}|残り重量{2})", item_bom, item_bom * 2,item);
                            choose_item_bom = false;
                        }
                        else
                        {
                            Console.WriteLine("数値が正しくありません。");
                        }
                    }
                }

                bool choose_item_bullet = true;
                while (choose_item_bullet == true)
                {
                    Console.WriteLine("2:弾薬 を何個持っていきますか?");
                    if (!int.TryParse(Console.ReadLine(), out item_bullet))
                    {
                        Console.WriteLine("半角数値を入力してください");
                    }
                    else
                    {
                        if (item - item_bullet >= 0)
                        {
                            item -= item_bullet;
                            Console.WriteLine("{0}個(重さ{1}|残り重量{2})", item_bullet, item_bullet,item);
                            choose_item_bullet = false;
                        }
                        else
                        {
                            Console.WriteLine("数値が正しくありません。");
                        }
                    }
                }

                bool choose_item_cure = true;
                while (choose_item_cure == true)
                {
                    Console.WriteLine("3:医療品 を何個持っていきますか?");
                    if (!int.TryParse(Console.ReadLine(), out item_cure))
                    {
                        Console.WriteLine("半角数値を入力してください");
                    }
                    else
                    {
                        if (item - item_cure >= 0)
                        {
                            item -= item_cure;
                            Console.WriteLine("{0}個(重さ{1}|残り重量{2})", item_cure, item_cure, item);
                            choose_item_cure = false;
                        }
                        else
                        {
                            Console.WriteLine("数値が正しくありません。");
                        }
                    }
                }

                Console.WriteLine("手榴弾{0}個、弾丸{1}個、医療品{2}個でよろしいですか?)", item_bom, item_bullet, item_cure);
                bool choose_item_last = true;
                while (choose_item_last == true)
                {
                    Console.WriteLine("1:はい/2:いいえ");
                    command = Console.ReadLine();
                    if (command == "1")
                    {
                        choose_item_last = false;
                        choose_item = false;
                        Console.WriteLine("");
                    }
                    else if (command == "2")
                    {
                        choose_item_last = false;
                        Console.WriteLine("もう一度再設定します。");
                        //アイテム数の初期化
                        item = 12;
                        item_bom = 0;
                        item_bullet = 0;
                        item_cure = 0;
                    }
                    else
                    {
                        Console.WriteLine("入力が正しくありません。");
                    }
                    Console.WriteLine("");
                }
            }

            //ロード地点(1)
            label1:
            section = 1;
            
            Console.WriteLine("移動中…");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「ん～」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「外は涼しくて気持ちいいねぇ～」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「さて、今日はどこに行こうかなぁ」");
            Console.ReadLine();//次のコメントを表示

            bool where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どちらへ行きますか?");
                Console.WriteLine("1:秘密の森");
                Console.WriteLine("2:輝く湖畔");
                Console.WriteLine("3:怪しい影");
                Console.WriteLine("----------");
                Console.WriteLine("4:セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                    where = true;
                        lack += 5;
                        Console.WriteLine("珍しい植物を見つけた！");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "2":
                    where = true;
                        Console.WriteLine("特にめぼしい物は見当たらなかった。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "3":
                    where = true;
                        Console.WriteLine("怪しい影に近づいてみると、何かはわからないが、中型の生物のように見える。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("こちらに対して敵対的な視線を向けて、今すぐにでも攻撃してきそうだ。");
                        Console.ReadLine();//次のコメントを表示
                        lack -= 5;
                        Console.WriteLine(" -+-+- 戦闘開始 -+-+- ");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("未実装です。");
                        //戦闘処理(未実装)
                        break;

                    case "4":
                        //セーブ処理
                        Save();
                        break;

                    default:
                        Console.WriteLine("半角数字を入力してください。");
                        break;
                }
            }

            //ロード地点(1)
            label2:
            section = 2;

            Console.WriteLine("更に森の奥を進んでいく。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("すると、山の斜面に石の建造物らしきものが見えた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「あれは…何かしらね。」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("ここまで遠くに来たのは今日が初めてだ。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("また、こんな森の奥まで来る人はほとんどいないだろう。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「怪しいわね…」");
            Console.ReadLine();//次のコメントを表示

            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どうしますか");
                Console.WriteLine("1:");
                Console.WriteLine("2:");
                Console.WriteLine("3:");
                Console.WriteLine("----------");
                Console.WriteLine("4:セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        Console.WriteLine("未実装です。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("未実装です。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "3":
                        where = true;
                        Console.WriteLine("未実装です。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "4":
                        //セーブ処理
                        Save();
                        break;

                    default:
                        Console.WriteLine("半角数字を入力してください。");
                        break;
                }
            }

            break;

        //ロード
        case "2":
            playing = false;

            //新システム

            //ファイル読み込み
            XDocument file_read = XDocument.Load(@"./Data.xml");

            IEnumerable<XElement> datas = file_read.Elements("Data");
            foreach (XElement data_read in datas)
            {

                section = int.Parse(data_read.Element("Section").Value);
                lack = int.Parse(data_read.Element("Luck").Value);
                item = int.Parse(data_read.Element("item").Value);
                item_bom = int.Parse(data_read.Element("item_bom").Value);
                item_bullet = int.Parse(data_read.Element("item_bullet").Value);
                item_cure = int.Parse(data_read.Element("item_cure").Value);

                Console.WriteLine("ロードが成功しました。");
                Console.ReadLine();//次のコメントを表示
                switch (section)
                {
                    case 1:
                        goto label1;

                    case 2:
                        goto label2;

                    default:
                        break;
                }
            }

            /*
            //ファイル読み込み
            XElement file_read = XElement.Load(@"./Data.xml");

            //データのタグ内の情報を取得する
            IEnumerable<XElement> data_read = from read in file_read.Elements("Data") select read;

            //データ情報分(セーブデータの数(←未来への拡張用))ループして、表示
            foreach (XElement temp1 in data_read)//ここが動作していない
            {
                section = int.Parse(temp1.Element("Section").Value);
                lack = int.Parse(temp1.Element("Luck").Value);
                item = int.Parse(temp1.Element("item").Value);
                item_bom = int.Parse(temp1.Element("item_bom").Value);
                item_bullet = int.Parse(temp1.Element("item_bullet").Value);
                item_cure = int.Parse(temp1.Element("item_cure").Value);

                //臨時
                Console.WriteLine(temp1.Element("Section"));
                Console.WriteLine(section);
                //ここまで
            }
            */

            //ファイルを開きっぱなしだと上書き保存ができない(「さいしょから」→「保存」はできるが、「ロード」→「保存」ができない)ので、ファイルを閉じる
            //file_read.RemoveAll();//これでOKなのかな?


            /*旧バージョン
            //読み込むファイル(最重要なスタート位置のみロード。他は後で実装予定(っていうか、他もちゃんと実装しないと動かない…))
            StreamReader? file_read = new(@"./Data_main.aki1");
                string? line = file_read.ReadLine();
            //ファイルを開きっぱなしだと上書き保存ができない(「さいしょから」→「保存」はできるが、「ロード」→「保存」ができない)ので、ファイルを閉じる
            file_read.Close();
            Console.WriteLine("ロードが成功しました。");
            Console.ReadLine();//次のコメントを表示
            switch (line)
            {
                case "1":
                    goto label1;

                case "2":
                    goto label2;

                default:
                    break;
            }
            */
            break;

        case "3":
            playing = false;
            break;

        default:
            Console.WriteLine("誤ったコマンドを入力しています。");
            break;
    }
}

//セーブ処理
void Save()
{
    //データ保存(xml)
    XElement element = new
        (
            "Data",
            new XElement("Section", section),
            new XElement("Luck", lack),
            new XElement("item", item),
            new XElement("item_bom", item_bom),
            new XElement("item_bullet", item_bullet),
            new XElement("item_cure", item_cure)
        );
    element.Save("Data.xml");
    Console.WriteLine("セーブが完了しました。");
}

/*旧セーブ処理
void Save()
{
    //メインデータ保存
    int[] main_Data = new int[] { section, lack };
    try
    {
        //独自拡張子aki1("Aki"duki version."1")
        StreamWriter file = new(@"./Data_main.aki1", false, Encoding.UTF8);
        for (int i = 0; i < main_Data.Length; i += 1)
        {
            file.WriteLine(string.Format("{0}", main_Data[i]));
        }
        file.Close();
        Console.WriteLine("セーブが完了しました。");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message); // 例外検出時にエラーメッセージを表示
    }

    //アイテム保存
    以下は今のところ使わないので封印
    int[] item_Data = new int[]{item, item_bom, item_bullet, item_cure};
    try
    {
        StreamWriter file = new(@"./Data_item.aki1", false, Encoding.UTF8);
        for (int i = 0; i < item_Data.Length; i += 1)
        {
            file.WriteLine(string.Format("{0}", item_Data[i]));
        }
        file.Close();
        Console.WriteLine("セーブが完了しました。");
        Console.WriteLine("");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message); // 例外検出時にエラーメッセージを表示
    }
}
*/
