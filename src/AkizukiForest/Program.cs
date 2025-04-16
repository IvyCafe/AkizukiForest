using AnovSyntax;
using System.Xml.Linq;

namespace AkizukiForest;

static class Program
{
    static int Main(string[] args)
    {
        // タイトル設定
        Console.Title = "Akizuki in the Forest";

        Console.WriteLine("# Akizuki in the Forest #");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Hint: Ctrl+C でいつでも終了できます。");
        Console.ResetColor();
        Console.WriteLine("");

        // 終了時に文字色をもとに戻す
        Console.CancelKeyPress += (sender, e) =>
        {
            Console.ResetColor();
        };

        // 初期化
        bool where;             // ループ用
        int section = 0;        // セーブ用
        string? command;        // コマンド読み取り

        // ステータス
        Character komari = new()
        {
            Name = "秋月小鞠",
            HP = 200,
            MP = 30,
            Luck = 10
        };

        // アイテム
        int item = 12;          // アイテムの総重量
        int itemBom = 0;        // 手榴弾の数 (手榴弾の重さはx2で算出)
        int itemBullet = 0;     // 弾丸の数
        int itemMedicine = 0;   // 医療品の数

        if (File.Exists("Data.xml"))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("セーブデータが見つかりました。");
            Console.ResetColor();

            Console.Write("セーブデータを読み込みますか? (Y/n): ");

            command = Console.ReadLine();
            Console.WriteLine("");
            if (command?.ToLower() == "n" || command?.ToLower() == "no")
            {
                Console.WriteLine("セーブデータを読み込まず、開始します。");
            }
            else
            {
                Console.WriteLine("セーブデータを読み込みます。");
                Console.WriteLine("");

                try
                {
                    Load();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ロードが成功しました。");
                    Console.ResetColor();
                }
                catch
                {
                    Console.WriteLine("ロードが失敗しました。");
                }
            }
            Console.ReadLine();
        }

        switch (section)
        {
            case 0:
                InitialSettings();
                break;
    
            case 1:
                Section1();
                break;
    
            case 2:
                Section2();
                break;
    
            case 3:
                Section3();
                break;
    
            case 6:
                Section6();
                break;

            default:
                WarningConsole("セーブデータが破損しています。");
                return 1;
        }

        return 0;

        void InitialSettings()
        {
            AnovReader("0.anov");

            // アイテム選択処理
            bool selectingItem = true;
            while (selectingItem == true)
            {
                Console.WriteLine("");
                Console.WriteLine("その他に冒険に持っていくものを決めてください");
                Console.WriteLine("");
                Console.WriteLine("持っていけるもの (最大12)");
                Console.WriteLine("1: 手榴弾 (攻撃/爆破) [1つ当たりの重さ: 2]");
                Console.WriteLine("2: 弾薬 (攻撃) [1セット (10発) 当たりの重さ: 1]");
                Console.WriteLine("3: 医療品 (回復) [1つ当たりの重さ: 1]");
                Console.WriteLine("");

                ItemSelection("1: 手榴弾", ref item, ref itemBom, 2);
                ItemSelection("2: 弾薬", ref item, ref itemBullet, 1);
                ItemSelection("3: 医療品", ref item, ref itemMedicine, 1);

                void ItemSelection(string _itemName, ref int _item, ref int _itemEach, int _itemWeight)
                {
                    bool selectingEachItems = true;
                    while (selectingEachItems == true)
                    {
                        Console.WriteLine(_itemName + " を何個持っていきますか?");
                        if (int.TryParse(Console.ReadLine(), out _itemEach) && (_item - _itemEach * _itemWeight >= 0))
                        {
                            _item -= _itemEach * _itemWeight;
                            Console.WriteLine("{0}個 (重さ{1}|残り重量{2})", _itemEach, _itemEach * _itemWeight, _item);
                            selectingEachItems = false;
                        }
                        else
                            InvalidInput();
                    }
                }

                Console.WriteLine($"手榴弾{itemBom}個、弾丸{itemBullet}個、医療品{itemMedicine}個でよろしいですか? (Y/n): ");

                command = Console.ReadLine();
                Console.WriteLine("");
                if (command?.ToLower() == "n" || command?.ToLower() == "no")
                {
                    WarningConsole("もう一度設定します。");
                    // アイテム数の初期化
                    item = 12;
                    itemBom = 0;
                    itemBullet = 0;
                    itemMedicine = 0;
                }
                else
                {
                    selectingItem = false;
                    // 現時点では、弾丸のセット数を記録しているため、弾数表記に変更
                    itemBullet *= 10;
                }
            }

            Section1();
        }

        void Section1()
        {
            section = 1;

            AnovReader("1.anov");

            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どちらへ行きますか?");
                Console.WriteLine("1: 秘密の森");
                Console.WriteLine("2: 輝く湖畔");
                Console.WriteLine("3: 怪しい影");
                Console.WriteLine("----------");
                Console.WriteLine("4: セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        komari.Luck += 5;
                        Console.WriteLine("珍しい植物を見つけた！");
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("特にめぼしい物は見当たらなかった。");
                        Console.ReadLine();
                        break;

                    case "3":
                        where = true;
                        Console.WriteLine("怪しい影に近づいてみると、何かはわからないが、中型の生物のように見える。");
                        Console.ReadLine();
                        Console.WriteLine("こちらに対して敵対的な視線を向けて、今すぐにでも攻撃してきそうだ。");
                        Console.ReadLine();
                        komari.Luck -= 5;
                        // 戦闘処理
                        Buttle(komari);
                        break;

                    case "4":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

            Section2();
        }

        void Section2()
        {
            section = 2;

            AnovReader("2.anov");

            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どうしますか");
                Console.WriteLine("1: すぐに建造物に近づく");
                Console.WriteLine("2: 建造物の周りを調べる");
                Console.WriteLine("3: 一旦木陰で休憩する");
                Console.WriteLine("----------");
                Console.WriteLine("4: セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        Console.WriteLine("見たことがない建物を見た秋月は、すぐにそこへ近づいた。");
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("建造物の周辺を見渡すと、近くには紅の花と、鮮やかな秘色(ひそく)の花が咲き乱れていた。");
                        // 特に意味はないが、花の紹介 (後付け設定)

                        // 紅の花: ゼラニウム (天竺葵-てんじくあおい)
                        // 4月~6月開花の四季咲き植物。北大陸は冬の寒さが厳しいので冬は散ってしまう。
                        // 花言葉: 信頼 真の友情 尊敬 決心 | (外国) 愚かさ 良い育ち 上流階級 | (赤色) 君ありて幸福 あなたがいて幸せ
                        // ハンガリーの国花。

                        // 秘色の花: ワスレナグサ (勿忘草)
                        // 3月下旬~4月に見られる。(北大陸は夏でもそれほど気温が上がらないため (もちろん地域による差はあるが)、夏設定の本作品でもつじつまは合う。)
                        // 花言葉: 私を忘れないで 真実の愛
                        Console.ReadLine();
                        Console.WriteLine("微かに香る花の香りで癒された。");
                        Console.ReadLine();
                        komari.Luck += 3;
                        komari.HP += 20; // 微妙に回復 (本作では HP の最大値はない (しいて言うなら int 32bit が最大値))
                        Console.WriteLine("先ほどの建物ことをふと思い出した。");
                        Console.ReadLine();
                        Console.WriteLine("こんな森の奥に誰が作ったのかが気になり、気が付くとその足はそちらへと動いていた。");
                        Console.ReadLine();
                        break;

                    case "3":
                        where = true;
                        Console.WriteLine("一旦木陰で休憩することにした。");
                        Console.ReadLine();
                        Console.WriteLine("森林ならではの爽やかな風が頬に当たり心地が良い。");
                        Console.ReadLine();
                        Console.WriteLine("視界、音、匂い。それらすべての要素が私の気分をとても穏やかにさせた。");
                        Console.ReadLine();
                        komari.HP += 50;
                        komari.MP += 10;
                        Console.WriteLine("先ほどの建物ことをふと思い出した。");
                        Console.ReadLine();
                        Console.WriteLine("こんな森の奥に誰が作ったのかが気になり、気が付くとその足はそちらへと動いていた。");
                        Console.ReadLine();
                        break;

                    case "4":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

            Section3();
        }

        void Section3()
        {
            section = 3;

            Console.WriteLine("石で建造されたトンネルのようなその建造物は、山の内部へ続いている。");
            Console.ReadLine();
            Console.WriteLine("トンネルへ入り奥へ進んでいくと、道は二股に別れていた。");
            Console.ReadLine();
            Console.WriteLine("道は、両方とも壁掛けのたいまつで照らされていて、");
            Console.WriteLine("右の道は細い道が、左の道は曲がりくねった道が続いていた。");
            Console.ReadLine();
            Console.WriteLine("「うーん…どっちに行こうかなぁ」");
            Console.ReadLine();

            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どちらに行きますか");
                Console.WriteLine("1: 右の道");
                Console.WriteLine("2: 左の道");
                Console.WriteLine("----------");
                Console.WriteLine("3: セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        Console.WriteLine("右の道へ進んでいくと、その道はだんだんと細くなってきた。");
                        Console.ReadLine();
                        Console.WriteLine("だんだんと地下へと進んでいった。");
                        Console.ReadLine();
                        Console.WriteLine("「さ、寒いわね…」");
                        Console.ReadLine();
                        Console.WriteLine("夏とはいえ、この地域は地上でも涼しいため、地下では寒いほどになってきた。");
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("道なりに進んでいくと、何かの影のようなものが見えた。");
                        Console.ReadLine();
                        Console.WriteLine("「なにかしら？」");
                        Console.ReadLine();
                        Console.WriteLine("隠れながらのぞいてみると、そこには異様な機影が見えた。");
                        Console.ReadLine();
                        Console.WriteLine("こちらにはまだ気づいていないようだ。");
                        Console.ReadLine();

                        // 戦闘or道を戻って分岐点の右側の道に行くかの選択肢
                        bool which = false;
                        while (which == false)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("どうしますか");
                            Console.WriteLine("1: 敵と戦闘する");
                            Console.WriteLine("2: 先ほどの分岐点まで戻る");

                            command = Console.ReadLine();
                            switch (command)
                            {
                                case "1":
                                    which = true;
                                    //戦闘処理
                                    Buttle(komari);
                                    break;

                                case "2":
                                    which = true;
                                    where = false;
                                    Console.WriteLine("先ほどの分岐点まで戻った。");
                                    Console.ReadLine();
                                    break;

                                default:
                                    InvalidInput();
                                    break;
                            }
                        }
                        break;

                    case "3":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

            AnovReader("3.anov");

            // 強制戦闘のため、自動回復
            komari.HP = (komari.HP >= 260) ? komari.HP : 260;
            komari.MP = (komari.MP >= 30) ? komari.MP : 30;
            // 戦闘開始
            Buttle(komari);

            Section6();
        }

        void Section6()
        {
            section = 6;

            AnovReader("4.anov");

            // 下の階に行くか、上の階に行くかで大きく√分岐
            // 天空世界の頂上到達√…天空世界の結末(たまたま地下洞窟と天空世界でつながってしまったことなど)を明かす
            // 天空世界の地上到達√…地下空洞の過去を明かす
            // 秘密コマンドにて窓から飛び降りる→森の泉(隠し)√…次回作や尾花と桜(北大陸)の世界観についてのフラグ・ヒント/秋月の過去について
            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どうしますか");
                Console.WriteLine("1: 上の階へ行く");
                Console.WriteLine("2: 下の階へ行く");
                // 3: 窓から飛び降りる (隠し√)
                // 3番を隠すことでフラグにしている。
                // (隠し√だけど、3番が飛ばされているというのは違和感があるし、結構見つけやすい√だと思う。)
                // (3番を飛ばしているのはただのミスだと思われるかもしれないけど)
                Console.WriteLine("----------");
                Console.WriteLine("4: セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        // 天空ルート(戦闘(ボス戦)後に神が現れて現世に戻らせてくれる)

                        AnovReader("4.1.1.anov");

                        // (秋月回復)
                        komari.HP = (komari.HP >= 300) ? komari.HP : 300;
                        komari.HP += 240;
                        komari.MP += 10;
                        // 戦闘開始 (ボス戦)
                        Buttle(komari, 1000, "黒色の影");

                        AnovReader("4.1.2.anov");

                        komari.Luck += 5;

                        AnovReader("4.1.3.anov");

                        Console.WriteLine("");
                        Console.WriteLine("END: 天界の青空 (通常ルート1)");
                        Console.WriteLine("[天界の運命]");
                        Console.WriteLine("");
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        // 地上ルート (地下の場所に関する文献を読める。また、魔導書かワープ装置か何かで現世に戻る。)

                        AnovReader("4.2.anov");

                        // 探索ターン
                        // 最短√…机のメモを読む→本棚からメモに書いてある題名の本を探す→斜め読み→地上世界へ戻る魔法→終了 (神には遭遇しない)
                        bool which = false;
                        bool note = false;
                        while (which == false)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("どこを調べますか");
                            Console.WriteLine("1: 本棚");
                            Console.WriteLine("2: 机");
                            Console.WriteLine("3: 椅子");

                            command = Console.ReadLine();
                            switch (command)
                            {
                                case "1":
                                    Console.WriteLine("20冊以上の本が並んでいた。");
                                    Console.ReadLine();
                                    // 机のメモ帳を見ているか見ていないか
                                    if (note == false)
                                    {
                                        // 机のメモ帳を見ていないときの処理
                                        Console.WriteLine("右から1冊取り、読んでみたがいまいち意味が分からなかった。");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        // 机のメモ帳を見ているときの処理

                                        AnovReader("4.2.1.anov");

                                        which = true;
                                    }
                                    break;

                                case "2":
                                    Console.WriteLine("白い塗装が施されている机だ。");
                                    Console.ReadLine();
                                    Console.WriteLine("机上にはメモ帳と、ペンが1本ある。");
                                    Console.ReadLine();
                                    Console.WriteLine("メモ帳には次の文字が殴り書きされていた。");
                                    Console.ReadLine();
                                    Console.WriteLine("「天界のレートピシ」"); // レートピシ=歴史書
                                    Console.ReadLine();
                                    note = true;
                                    break;

                                case "3":
                                    Console.WriteLine("何一つ変哲のない木製の椅子だ。");
                                    Console.ReadLine();
                                    Console.WriteLine("座る部分は水色の布に白い糸で刺繍がされている。");
                                    Console.ReadLine();
                                    break;

                                default:
                                    InvalidInput();
                                    break;
                            }
                        }
                        Console.WriteLine("");
                        Console.WriteLine("END: 小屋の本 (通常ルート2)");
                        Console.WriteLine("[真実を記す手記]");
                        Console.WriteLine("");
                        Console.ReadLine();
                        break;

                    case "3":
                        where = true;

                        AnovReader("3.3.anov");

                        Console.WriteLine("");
                        Console.WriteLine("END: 森の泉 (隠しルート)");
                        Console.WriteLine("[秋月の過去と見えない事実]");
                        // 見えない事実 (現実)…泉にいる神の言っていることが意味不明&説明不足過ぎる
                        Console.WriteLine("");
                        Console.ReadLine();
                        break;

                    case "4":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

            // 運が若干高い場合は秋月と椿の会話について解放
            // この文章はぶっちゃけこのゲーム内でも、Ivy Cafeteria のシナリオを知っている人でもわからないと思う。
            // (親友=椿とわかる人はすごい。親友→×親、上司、△世界の終焉の前に。(長い間合っていないので一緒に喋る機会がない)、〇旧黒軍師
            // →旧黒軍師では3人いるが、航空隊2人の名前や詳細がまだ決まっていないため、椿という推測は一応可能だが。
            // (未来になって航空隊2人の名前が決まっても、決まっていなかった時代のストーリーということで未来になっても上記と同じ考察が可能。))
            if (komari.Luck >= 8)
            {
                AnovReader("5.0.anov");
            }

            Console.WriteLine("# クレジット #");
            Console.WriteLine("シナリオ: Lemon73 (Ivy Cafeteria)");
            Console.WriteLine("プログラム: Lemon73 (Ivy Cafeteria)");
            Console.WriteLine("ベース: Console App (C#, .NET8.0) - Microsoft");
            Console.WriteLine("");
            Console.WriteLine("ゲームクリアです!遊んでいただき、ありがとうございました。");
            Console.ReadLine();
            // 裏話…手榴弾を実装したのは、実はトンネル内にふさがっているところがあって、そこを爆破して進んでいくためだったんだけど、
            // トンネルの話が長くなりすぎてつまらなかったから辞めた。だから、攻撃と逃げるときの両方で使えるくらいしかメリットはない
            // っていうか、手榴弾はともかく、銃の弾丸は普通に余るな…
            // 医療品なんかそもそも使わなくても魔法回復できるし… (戦闘前のMP回復のおかげでほぼMP切れにならないし)
            // 難易度調整はミスった (簡単すぎた) かも (だからといって今から調整するのも面倒)
        }

// セーブ処理
void Save()
{
    //データ保存(xml)
    XElement element = new
    (
        "Data",
        new XElement("Section", section),
        new XElement("komariHP", komari.HP),
        new XElement("komariMP", komari.MP),
        new XElement("KomariLuck", komari.Luck),
        new XElement("Item", item),
        new XElement("ItemBom", itemBom),
        new XElement("ItemBullet", itemBullet),
        new XElement("ItemMedicine", itemMedicine)
    );
    element.Save("Data.xml");
    Console.WriteLine("セーブが完了しました。");
    Console.WriteLine("終了する際は、Ctrl+C を押してください。");
}

// ロード処理
void Load()
{
    //ファイル読み込み
    XDocument dataLoading = XDocument.Load(@"./Data.xml");

    IEnumerable<XElement> datas = dataLoading.Elements("Data");
    foreach (XElement data in datas)
    {
        section         = int.TryParse(data.Element("Section")?.Value, out var tempSection) ? tempSection : 0;
        komari.HP       = int.TryParse(data.Element("komariHP")?.Value, out int tempKomariHP) ? tempKomariHP : 200;
        komari.MP       = int.TryParse(data.Element("komariMP")?.Value, out int tempKomariMP) ? tempKomariMP : 20;
        komari.Luck     = int.TryParse(data.Element("KomariLuck")?.Value, out int tempKomariLuck) ? tempKomariLuck : 10;
        item            = int.TryParse(data.Element("Item")?.Value, out int tempItem) ? tempItem : 0;
        itemBom         = int.TryParse(data.Element("ItemBom")?.Value, out int tempItemBom) ? tempItemBom : 2;
        itemBullet      = int.TryParse(data.Element("ItemBullet")?.Value, out int tempItemBullet) ? tempItemBullet : 4;
        itemMedicine    = int.TryParse(data.Element("ItemMedicine")?.Value, out int tempItemMedicine) ? tempItemMedicine : 4;
    }
}

//戦闘処理
void Buttle(Character chara, int enemyHP = 0, string enemyName = "異形の存在")
{
    Random rand = new();
    // HP が設定されていないときはランダム値に設定
    if (enemyHP == 0)
        enemyHP = 100 + rand.Next(0, 15) * 10; // 100-240 (10刻み)

    // 戦闘開始時自動回復 (難易度調節)
    chara.HP += 20 + rand.Next(0, 3) * 10; // 20-40 (10刻み)
    chara.MP += rand.Next(2, 6); // 2-5

    Console.WriteLine(" -+-+- 戦闘開始 -+-+- ");
    Console.ReadLine();

    while (enemyHP > 0)
    {
        Console.WriteLine(" - あなたのターン - ");
        Console.ReadLine();
        StatusConsole();

        bool where = false;
        while (where == false)
        {
            Console.WriteLine("");
            Console.WriteLine("どのような行動をしますか");
            Console.WriteLine("1: ナイフ攻撃");
            Console.WriteLine("2: 手榴弾攻撃 (残り{0}個)", itemBom);
            Console.WriteLine("3: 拳銃射撃 (残り{0}発)", itemBullet);
            Console.WriteLine("4: 治療魔法"); // MPを使って回復
            Console.WriteLine("5: 回復薬治療 (残り{0}個)", itemMedicine);
            Console.WriteLine("6: 戦略的撤退"); // 逃げる
            Console.WriteLine("7: 手榴弾退散 (残り{0}個)", itemBom); // 手榴弾の爆破と同時に逃げることで「戦略的撤退」よりも高確率で逃げ切れる
            Console.WriteLine("8: 敵味方のステータスを再確認");

            command = Console.ReadLine();
            switch (command)
            {
                // 近接戦闘
                case "1":
                    where = true;
                    Console.WriteLine("敵に接近して、ナイフを振り上げて…");
                    Console.WriteLine("その腕を素早く振り降ろした。");
                    Console.ReadLine();
                    Console.WriteLine("敵はダメージを受けたようだ。");
                    Console.ReadLine();
                    enemyHP -= 50; // 固定ダメージ
                    break;

                // 爆破攻撃
                case "2":
                    if (itemBom <= 0)
                    {
                        WarningConsole("爆弾がないようだ。");
                        break;
                    }

                    where = true;
                    itemBom -= 1;
                    Console.WriteLine("秋月は後ろずさりで少しずつ後退しながら、手榴弾のピンを抜いて、");
                    Console.WriteLine("敵のほうに向かって投げつけた。");
                    Console.ReadLine();
                    Console.WriteLine("しばらくすると、大きな爆発音を上げ、付近は白い煙に包まれた。");
                    Console.ReadLine();
                    enemyHP -= 150 + rand.Next(0, 4) * 10; // 150~180 (10刻み)
                    break;

                // 射撃攻撃処理
                case "3":
                    // (ナイフ/爆弾との違いのために命中率は低く、ダメージが大きい、貫通するとさらにダメージ増加とかのほうがいいかも。
                    // ↑一応、弾薬数が多いため攻撃可能回数がほぼ無限というメリットもあるけど。)
                    if (itemBullet <= 0)
                    {
                        WarningConsole("弾薬が足りないようだ。");
                        break;
                    }

                    where = true;
                    itemBullet -= 1;
                    Console.WriteLine("拳銃に弾丸を込め、単発射撃を行った。");
                    Console.ReadLine();
                    Console.WriteLine("拳銃は、爆音を上げて、強い反動を受けた。");
                    Console.ReadLine();
                    Console.WriteLine("弾丸は敵を直撃して、敵の体を貫通した。");
                    Console.ReadLine();
                    enemyHP -= 40 + rand.Next(0, 27) * 10; // 40~300 (10刻み)
                    break;

                // 治療処理
                case "4":
                    where = true;
                    if (chara.MP >= 3)
                    {
                        chara.MP -= 3;
                        Console.WriteLine("「リヴァレド・ハート」");
                        Console.WriteLine("両手を空にかざし、回復魔法を唱えた。");
                        Console.ReadLine();
                        Console.WriteLine("優しいオーラが秋月の体を包み込む。");
                        Console.ReadLine();
                        Console.WriteLine("傷は見る見るうちに回復していった。");
                        Console.ReadLine();
                        chara.HP += 150; // 固定
                    }
                    else
                    {
                        Console.WriteLine("魔力が足りないため、簡易的な治療のみ行うことにした。");
                        Console.ReadLine();
                        Console.WriteLine("ナイフでスカートの先を切り、その布で傷ついた部分を保護した。");
                        Console.ReadLine();
                        chara.HP += 40; // 固定
                    }
                    break;

                // アイテム治療処理
                case "5":
                    if (itemMedicine <= 0)
                    {
                        WarningConsole("治療薬がないようだ。");
                        break;
                    }

                    where = true;
                    itemMedicine -= 1;
                    Console.WriteLine("医療品をあさり、治療に使えそうな薬品などを取り出した。");
                    Console.ReadLine();
                    Console.WriteLine("傷ついた部分に治療薬を塗り、包帯で巻いて痛みを和らげることができた。");
                    Console.ReadLine();
                    chara.HP += 220; // 固定
                    break;

                case "6":
                    where = true;
                    // 確立で逃げる処理 (20%の確率で逃げられる)
                    if (rand.Next(0, 101) <= 20)
                    {
                        Console.WriteLine("敵からなんとか逃げ切ることができた。");
                        Console.ReadLine();
                        enemyHP = 0;
                    }
                    else
                        WarningConsole("敵に回り込まれた。");
                    break;

                case "7":
                    // 手榴弾があるかを確認
                    if (itemBom <= 0)
                    {
                        WarningConsole("爆弾がないようだ。");
                        break;
                    }

                    where = true;
                    itemBom -= 1;
                    Console.WriteLine("秋月は後ろずさりで少しずつ後退しながら、手榴弾のピンを抜いて、");
                    Console.WriteLine("明後日のほうに向かって投げつけた。");
                    Console.ReadLine();
                    Console.WriteLine("しばらくすると、大きな爆発音を上げ、付近は白い煙に包まれた。");
                    Console.ReadLine();
                    Console.WriteLine("煙に紛れて逃げようと試みる。");
                    Console.ReadLine();

                    // 確立で逃げる処理 (60%の確率で逃げられる)
                    if (rand.Next(0, 101) <= 60)
                    {
                        Console.WriteLine("なんとか逃げ切ることができた。");
                        Console.ReadLine();
                        enemyHP = 0;
                    }
                    else
                        WarningConsole("しかし、敵に回り込まれてしまった。"); // 敵にダメージはなし
                    break;

                case "8":
                    StatusConsole();
                    break;

                default:
                    InvalidInput();
                    break;
            }

            // 敵死亡判定
            if (enemyHP <= 0)
            {
                if (command != "6" && command != "7")
                {
                    // 逃げた場合以外表示
                    Console.WriteLine("敵はその場に倒れ、もう二度と動かなくなった。");
                    Console.ReadLine();
                }

                Console.WriteLine(" -+-+- 戦闘終了 -+-+- ");
                Console.ReadLine();
                Console.WriteLine("「ふう。何とかなったわね。」");
                Console.ReadLine();
                // Console.WriteLine("「先へ進むわ。」");
                // Console.ReadLine();
            }
            else
            {
                // もし攻撃 or 回復 (逃げる+誤入力以外) なら
                // この if 文で↓のバグを回避できる。
                // (8の「様子を見る」や誤コマンドの際に本来なら再度コマンドを選べるはずなのに、すぐに敵ターンに移行してしまうバグ。
                // 原因: while の中に敵の処理を入れているから)
                if (where == true)
                {
                    // 続行 (敵ターン)
                    Console.WriteLine(" - 敵のターン - ");
                    Console.ReadLine();

                    // 70%
                    if (rand.Next(10) < 7)
                    {
                        Console.WriteLine("敵からの攻撃");
                        Console.ReadLine();
                        chara.HP -= rand.Next(5, 14) * 10; // 50~130 (10刻み) (最大値が出るとまあまあ強いので HP に注意)
                    }
                    // 20%
                    else if (rand.Next(3) != 0)
                    {
                        Console.WriteLine("敵は様子を見ている。");
                        Console.ReadLine();
                    }
                    // 10%
                    else
                    {
                        Console.WriteLine("敵は回復魔法を唱えた。");
                        Console.ReadLine();
                        enemyHP += 120; // 固定
                    }

                    // 秋月死亡判定
                    if (chara.HP <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // 文字の色を変更
                        Console.WriteLine("");
                        Console.WriteLine("「う、うそ…」");
                        Console.ReadLine();
                        Console.WriteLine("体に大きな傷を負った秋月はもう、そこから動くことができなくなってしまった。");
                        Console.ReadLine();
                        Console.WriteLine("視界に広がる真紅の液体。それが自分の運命を示唆していた。");
                        Console.ReadLine();
                        Console.WriteLine("「こ…こんなところで死ぬはずは…」");
                        Console.ReadLine();
                        Console.WriteLine("言葉を言い終える前に、その人生の終わりを早々と迎えた。");
                        Console.ReadLine();
                        Console.ResetColor(); // 色をリセット
                        // 終了判定
                        enemyHP = 0;
                        Environment.Exit(0);
                    }
                }
            }
        }
    }

    // キャラクターステータス表示
    void StatusConsole(){
        Console.WriteLine("");
        Console.WriteLine("味方ステータス");
        Console.WriteLine($"|{chara.Name}|HP: {chara.HP}|MP: {chara.MP}|");
        Console.WriteLine("");
        Console.WriteLine("敵ステータス");
        Console.WriteLine($"|{enemyName}|HP: {enemyHP}|");
    }
}
}
    /// <summary>
    /// Reads the .anov file and outputs the text to the console.
    /// </summary>
    /// <param name="filePath">The path of .anov file</param>
    static void AnovReader(string filePath)
    {
        filePath = Path.Combine(AppContext.BaseDirectory, "story", filePath);

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"ファイル\"{filePath}\"が見つかりません。");
            Console.ReadLine();
            return;
        }

        using (StreamReader sr = new(filePath))
        {
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (line == "")
                    Console.ReadLine();
                else if (line is not null)
                    Console.WriteLine(Anov.Read(line, QuoteType.None));
            }
            Console.ReadLine();
        }

        return;
    }

    /// <summary>
    /// Writes an error message to the standard error stream in yellow color.
    /// </summary>
    static void InvalidInput()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Error.WriteLine("誤ったコマンドを入力しています。");
        Console.ResetColor();
    }

    /// <summary>
    /// Writes the specified string value, followed by the current line terminator, to the standard output stream in yellow color.
    /// </summary>
    /// <param name="message">Input string value</param>
    static void WarningConsole(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.ReadLine();
    }
}

/// <summary>
/// Represents a character in the game.
/// </summary>
public class Character
{
    /// <summary>
    /// Name of the character.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Hit Points (HP) of the character.
    /// </summary>
    public int HP { get; set; }
    /// <summary>
    /// Magic Points (MP) of the character.
    /// </summary>
    public int MP { get; set; }
    /// <summary>
    /// Luck of the character.
    /// </summary>
    public int Luck { get; set; }
}
