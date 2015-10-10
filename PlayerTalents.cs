using UnityEngine;
using System.Collections;

public class PlayerTalents : MonoBehaviour {
    public bool Declared = false;
    public string declaration = "Undeclared";

    public Talent[] Inorganic_Talents_t1 = new Talent[5];
    public Talent[] Organic_Talents = new Talent[40];
    public Talent[] Biolchem_Talents = new Talent[6];
    
    public class Talent
    {
        // visual data
        public string name;
        public string description;
        public bool mystery;
        public bool reachable;
        public bool visible;
        public bool gotten;
        public Texture2D icon;
        public Rect pos;
        public int itemUnlock;
        public int spellUnlock;
        public int cost;
        public int itemReq;
        public int spellReq;
        public int treeUnlock;
        public int treeUnlock2;
        public int treeUnlock3;
        public int treeMysteryUnlock1;
        public int treeMysteryUnlock2;
        public int treeMysteryUnlock3;
        public int tier;
    }

    private void setTalent(Talent c, string tname, string tdescription, bool tmystery, bool treachable, bool tvisible, bool tgotten, Texture2D ticon,
        Rect tpos, int cost, int titemunlock, int tspellunlock, int titemreq, int tspellreq, int unlock1, int unlock2,
        int unlock3, int mystery1, int mystery2, int mystery3, int t)
    {
        c.name = tname; c.description = tdescription; c.mystery = tmystery; c.reachable = treachable; c.gotten = tgotten; c.icon = ticon;
        c.pos = tpos; c.itemUnlock = titemunlock; c.spellUnlock = tspellunlock; c.itemReq = titemreq; c.spellReq = tspellreq;
        c.treeUnlock =  unlock1; c.treeUnlock2 = unlock2; c.treeUnlock3 = unlock3; c.treeMysteryUnlock1 = mystery1; c.treeMysteryUnlock2 = mystery2;
        c.treeMysteryUnlock3 = mystery3; c.visible = tvisible; c.cost = cost; c.tier = t;
    }

    public Texture2D methI, metholI, ethicon, ccl4icon, cf4icon, etholicon, propaneicon, isopropicon, propolicon, cfaneicon, dcmicon, octoicon, pentaneicon, hexicon;
    public Texture2D acetyleneicon, benzeneicon, orthotolueneicon, phenolicon, phenylacetyleneicon, ethyleneglycerolicon, tetraficon, propylglycolicon, diethglycolicon, phenylacetateicon;
    public Texture2D tolueneui, nitrotolueneui, trinitrotolueneui, nitrobenzoicacidui, benzoicacidui, benzotrichlorideui, benzoylchlorideui, rdxui, co2ui, octanitrocubaneui;
    public Texture2D so2ui, petnui, chloroformui, odfui, fluoroformui, octogenui;
    public Texture2D bronzesweepicon, bronzesweep2icon, bronzegreatswordicon, bronzedaggericon, lithiumicon;
	public Texture2D hemeicon, aspirinicon, ibuprofenicon, influenzaicon, chlorinegasicon, myoglobinicon;

	// Use this for initialization


	void Start () {
        Talent meth = new Talent();
        setTalent(meth,"Methane","Methane is the most simple organic molecule. Produces a small explosion that damages enemies with almost no cooldown. Most who own a synthus are able to produce this chemical with ease, making it good for beginners.", false, true, true, false, methI, new Rect(1920 / 2-275, 1080 / 2 -180, 50, 50),
            0, 0, 0, -1, -1, 1, 2, -1, -1, -1, -1, 1);

        Talent methol = new Talent();
        setTalent(methol, "Methanol", "Wood alcohol will produce a slightly bigger explosion than methane. It requires a slightly longer cooldown, however. Methanol easily burns in oxygen, but takes a bit longer than Methane to recharge because the synthus needs to take a bit of time to protect its Aluminum bits from its corrosive properties.", false, false, true, false, metholI, new Rect(1920 / 2 -275, 1080 / 2 - 80, 50, 50)
            , 300, 1, 1, 1, -1, 12, -1, -1, -1, -1, -1, 1);

        Talent eth = new Talent();
        setTalent(eth, "Ethane", "A very explosive odorless gas that can be concentrated by the synthus to give a very small but damaging explosion. Ethane, along with methane, is one of the two most abundant natural gases in the world. Many operations in the east were built to extract this precious gas.", false, false, true, false, ethicon, new Rect(1920 / 2 - 100, 1080 / 2 - 180, 50, 50), 400, -1, 2, -1, -1, 5, -1, -1, 3, -1, -1, 1);

        Talent ccl4 = new Talent();
        setTalent(ccl4, "Carbon Tetrachloride", "A very useful chemical that is known to extinguish fires and be extremely nonflammable. Can be utilized by the synthus to create a large icy mist that chills enemies.", true, false, false, false, ccl4icon, new Rect(1920 / 2 + 100, 1080 / 2 - 180, 50, 50), 600, -1, 3, 3, -1, 4, -1, -1, -1, -1, -1, 1);

        Talent cf4 = new Talent();
        setTalent(cf4, "Tetrafluoromethane", "An extremely stable molecule. The synthus may use it to concentrate surrounding water in the air into an icicle spear, which it could hurl at enemies.", false, false, false, false, cf4icon, new Rect(1920 / 2 + 100, 1080 / 2 + 120, 50, 50), 1000, -1, 4, -1, -1, 9, -1, -1, -1, -1, -1, 1);

        Talent ethol = new Talent();
        setTalent(ethol, "Ethanol", "Commonly known as 'alcohol', this molecule is known for its depressive properties. However, a number of 'researchers' (often regarded as drunks) in a northern Regia encampment discovered offensive uses for the chemical. It can be concetrated down into a deadly explosion.", false, false, true, false, etholicon, new Rect(1920 / 2 - 100, 1080 / 2 - 80, 50, 50), 1500, -1, 5, -1, -1, 6, -1, -1, -1, -1, -1, 1);

        Talent prop = new Talent();
        setTalent(prop, "Propane", "Propane is very simple to handle because it can be stored as a liquid but immediately turn gaseous when it is released. Because of this, the synthus can condense a great amount of propane in its combustion, making it a very powerful explosion.", false, false, true, false, propaneicon, new Rect(1920 / 2 - 100, 1080 / 2 + 120, 50, 50), 1500, -1, 6, -1, -1, 7, -1, -1, -1, -1, -1, 1);

        Talent isoprop = new Talent();
        setTalent(isoprop, "Isopropanol", "fire wall", false, false, true, false, isopropicon, new Rect(1920 / 2 - 100, 1080 / 2 - 180, 50, 50), 999999, -1, 7, -1, -1, 8, -1, -1, -1, -1, -1, 2);

        Talent propol = new Talent();
        setTalent(propol, "1-Propanol", "fire ring", false, false, true, false, propolicon, new Rect(1920 / 2 - 100, 1080 / 2 - 80, 50, 50), 5000, -1, 5, -1, -1, 14, -1, -1, -1, -1, -1, 2);

        Talent cfane = new Talent();
        setTalent(cfane, "Chlorofluoromethane", "frost nova", false, false, true, false, cfaneicon, new Rect(1920 / 2 + 100, 1080 / 2 - 180, 50, 50), 0, -1, 9, -1, -1, 10, 11, -1, -1, -1, -1, 2);

        Talent dcm = new Talent();
        setTalent(dcm, "Dichloromethane", "anivia wall", true, false, false, false, dcmicon, new Rect(1920 / 2 + 100, 1080 / 2 + 120, 50, 50), 0, -1, 10, -1, -1, -1, -1, -1, -1, -1, -1, 2);

        Talent octo = new Talent();
        setTalent(octo, "Octofluoropropane", "ice lance", false, false, true, false, octoicon, new Rect(1920 / 2 + 100, 1080 / 2 - 80, 50, 50), 0, -1, 11, -1, -1, 19, -1, -1, -1, -1, -1, 2);

        Talent pent = new Talent();
        setTalent(pent, "Pentane", "boom", false, false, true, false, pentaneicon, new Rect(1920 / 2 - 275, 1080 / 2 - 80, 50, 50), 0, -1, 12, -1, -1, 13, -1, -1, -1, -1, -1, 2);

        Talent hex = new Talent();
        setTalent(hex, "Hexane", "small boom", false, false, true, false, hexicon, new Rect(1920 / 2 - 275, 1080 / 2 + 120, 50, 50), 0, -1, 13, -1, -1, 15, -1, -1, -1, -1, -1, 2);

        Talent acetylene = new Talent();
        setTalent(acetylene, "Acetylene", "laser", false, false, true, false, acetyleneicon, top_center_left, 0, -1, 14, -1, -1, 19, 23, -1, -1, -1, -1, 3);

        Talent benzene = new Talent();
        setTalent(benzene, "Benzene", "boomer", false, false, true, false, benzeneicon, top_left_most, 0, -1, 15, -1, -1, 17, -1, -1, -1, -1, -1, 3);

        Talent orthotol = new Talent();
        setTalent(orthotol, "Orthotoluidine", "smallboomer", true, false, false, false, orthotolueneicon, bot_left_most, 0, -1, 16, -1, -1, -1, -1, -1, -1, -1, -1, 3);

        Talent phenol = new Talent();
        setTalent(phenol, "Phenol", "fireball", false, false, true, false, phenolicon, mid_left_most, 0, -1, 17, -1, -1, 16, 23, 24, 18, -1, -1, 3);

        Talent ethglyc = new Talent();
        setTalent(ethglyc, "Ethylene Glycerol", "icewave", false, false, true, false, ethyleneglycerolicon, mid_center_right, 0, -1, 19, -1, -1, 20, 22, -1, -1, -1, -1, 3);

        Talent tetraf = new Talent();
        setTalent(tetraf, "1,1,1,2-Tetrafluoroethane", "ice spear", false, false, true, false, tetraficon, top_right_most, 0, -1, 20, -1, -1, 21, -1, -1, -1, -1, -1, 3);

        Talent propglyc = new Talent();
        setTalent(propglyc, "Propyl Glycerol", "Ice Laser", false, false, true, false, propylglycolicon, mid_right_most, 0, -1, 21, -1, -1, -1, -1, -1, -1, -1, -1, 3);

        Talent diethglyc = new Talent();
        setTalent(diethglyc, "Diethylene Glycerol", "icicles", true, false, true, false, diethglycolicon, bot_right_most, 0, -1, 22, -1, 19, -1, -1, -1, -1, -1, -1, 3);

        Talent phenylac = new Talent();
        setTalent(phenylac, "Phenyl Acetylene", "Fire plumes", false, false, true, false, phenylacetyleneicon, bot_center_right, 0, -1, 23, -1, 17, -1, -1, -1, -1, -1, -1, 3);

        Talent toluene = new Talent();
        setTalent(toluene, "Toluene", "flame allover", false, false, true, false, tolueneui,top_center_left, 0, -1, 24, -1, 17, 25, 29, 28, -1, -1, -1, 4);

        Talent nitrotoluene = new Talent();
        setTalent(nitrotoluene, "p-Nitrotoluene", "flamethrower", false, false, true, false, nitrotolueneui, top_left_most, 0, -1, 25, -1, 24, 27, 31, -1, 26, -1, -1, 4);

        Talent trinitrotoluene = new Talent();
        setTalent(trinitrotoluene, "Trinitrotoluene", "speed", true, false, false, false, trinitrotolueneui, mid_left_most, 0, -1, 26, -1, -1, -1, -1, -1, -1, -1, -1, 4);

        Talent nitrobenzoicacid = new Talent();
        setTalent(nitrobenzoicacid, "3-Nitrobenzoic Acid", "smash", false, false, true, false, nitrobenzoicacidui, bot_left_most, 0, -1, 27, -1, -1, -1, -1, -1, -1, -1, -1, 4);

        Talent benzoicacid = new Talent();
        setTalent(benzoicacid, "Benzoic Acid", "conc", false, false, true, false, benzoicacidui, mid_center_left, 0, -1, 28, -1, -1, -1, -1, -1, -1, -1, -1, 4);

        Talent benzotrichloride = new Talent();
        setTalent(benzotrichloride, "Benzotrichloride", "spears", false, false, true, false, benzotrichlorideui, top_right_most, 0, -1, 29, -1, -1, 30, -1, -1, -1, -1, -1, 4);

        Talent benzoylchloride = new Talent();
        setTalent(benzoylchloride, "Benzoyl Chloride", "whooshes", false, false, true, false, benzoylchlorideui, mid_right_most, 0, -1, 30, -1, -1, 32, -1, -1, -1, -1, -1, 4);

        Talent rdx = new Talent();
        setTalent(rdx, "RDX", "huge boom", false, false, true, false, rdxui, top_center_left, 0, -1, 31, -1, -1, 37, -1, -1, 33, 35, -1, 5);

        Talent co2 = new Talent();
        setTalent(co2, "CO2", "frost armor", false, false, true, false, co2ui, top_right_most, 0, -1, 32, -1, -1, 34, -1, -1, 36, 38, -1, 5);

        Talent octanitrocubane = new Talent();
        setTalent(octanitrocubane, "Octanitrocubane", "comets", true, false, false, false, octanitrocubaneui, top_left_most, 0, -1, 33, -1, -1, -1, -1, -1, -1, -1, -1, 5);

        Talent so2 = new Talent();
        setTalent(so2, "SO2", "freezestorm", false, false, true, false, so2ui, mid_right_most, 0, -1, 34, -1, -1, -1, -1, -1, 36, -1, -1, 5);

        Talent DETN = new Talent();
        setTalent(DETN, "PETN", "living bomb", true, false, false, false, petnui, mid_left_most, 0, -1, 35, -1, -1, -1, -1, -1, -1, -1, -1, 5);

        Talent chloroform = new Talent();
        setTalent(chloroform, "Chloroform", "hailstorm", true, false, false, false, chloroformui, bot_center_right, 0, -1, 36, -1, -1, -1, -1, -1, -1, -1, -1, 5);

        Talent DDF = new Talent();
        setTalent(DDF, "DDF", "conc", false, false, true, false, odfui, bot_center_left, 0, -1, 37, -1, -1, -1, -1, -1, 39, -1, -1, 5);

        Talent fluoroform = new Talent();
        setTalent(fluoroform, "Fluoroform", "lance", true, false, false, false, fluoroformui, bot_right_most, 0, -1, 38, -1, -1, -1, -1, -1, -1, -1, -1, 5);

        Talent flameswathe = new Talent();
        setTalent(flameswathe, "Octogen", "flameswathe", true, false, false, false, octogenui, bot_left_most, 0, -1, 39, -1, -1, -1, -1, -1, -1, -1, -1, 5);

        Organic_Talents[0] = meth;
        Organic_Talents[1] = methol;
        Organic_Talents[2] = eth;
        Organic_Talents[3] = ccl4;
        Organic_Talents[4] = cf4;
        Organic_Talents[8] = propol;
        Organic_Talents[6] = prop;
        Organic_Talents[7] = isoprop;
        Organic_Talents[5] = ethol;
        Organic_Talents[9] = cfane;
        Organic_Talents[10] = dcm;
        Organic_Talents[11] = octo;
        Organic_Talents[12] = pent;
        Organic_Talents[13] = hex;
        Organic_Talents[14] = acetylene;
        Organic_Talents[15] = benzene;
        Organic_Talents[16] = orthotol;
        Organic_Talents[17] = phenol;
        Organic_Talents[18] = phenol;
        Organic_Talents[19] = ethglyc;
        Organic_Talents[20] = tetraf;
        Organic_Talents[21] = propglyc;
        Organic_Talents[22] = diethglyc;
        Organic_Talents[23] = phenylac;
        Organic_Talents[24] = toluene;
            Organic_Talents[25] = nitrotoluene;
            Organic_Talents[26] = trinitrotoluene;
            Organic_Talents[27] = nitrobenzoicacid;
            Organic_Talents[28] = benzoicacid;
            Organic_Talents[29] = benzotrichloride;
            Organic_Talents[30] = benzoylchloride;
            Organic_Talents[31] = rdx;
            Organic_Talents[32] = co2;
            Organic_Talents[33] = octanitrocubane;
            Organic_Talents[34] = so2;
            Organic_Talents[35] = DETN;
            Organic_Talents[36] = chloroform;
            Organic_Talents[37] = DDF;
            Organic_Talents[38] = fluoroform;
            Organic_Talents[39] = flameswathe;

        // inorganic

        Talent inor1 = new Talent();
        setTalent(inor1, "Sword Swing", "swoosh", false, true, true, false, bronzesweepicon, top_left_most, 0, -1, 40, -1, -1, 1, 2, -1, -1, -1, -1, 1);
        Inorganic_Talents_t1[0] = inor1;

        Talent ss2 = new Talent();
        setTalent(ss2, "Bronze Sword Swing 2", "swish", false, false, true, false, bronzesweep2icon, bot_left_most, 1000, -1, 41, -1, -1, -1, -1, -1, -1, 2, -1, 1);
        Inorganic_Talents_t1[1] = ss2;

        Talent bronzegs = new Talent();
        setTalent(bronzegs, "Bronze Greatsword", "whoop", true, false, false, false, bronzegreatswordicon, mid_left_most, 600, -1, 42, -1, -1, -1, -1, -1, -1, -1, -1, 1);
        Inorganic_Talents_t1[2] = bronzegs;

        Talent bronzedag = new Talent();
        setTalent(bronzedag, "Bronze Dagger", "swish", false, true, true, false, bronzedaggericon, top_center_left, 400, -1, 43, -1, -1, -1, -1, -1, -1, -1, -1, 1);
        Inorganic_Talents_t1[3] = bronzedag;

        Talent lithium = new Talent();
        setTalent(lithium, "Lithium Spark", "zap", true, true, true, false, lithiumicon, top_right_most, 5000, -1, 44, 4, -1, -1, -1, -1, -1, -1, -1, 1);
        Inorganic_Talents_t1[4] = lithium;

        // biochem

        Talent heme1 = new Talent();
        setTalent(heme1, "Hemoglobin", "Life drain", true, false, true, false, hemeicon, mid_left_most, 100, -1, 45, -1, -1, -1, -1, -1, -1, -1, -1, 1);
        Biolchem_Talents[0] = heme1;

        Talent aspirin = new Talent();
        setTalent(aspirin, "Aspirin", "HoT", false, true, true, false, aspirinicon, top_center_left, 600, -1, 46, -1, -1, 2, -1, -1, -1, -1, -1, 1);
        Biolchem_Talents[1] = aspirin;

        Talent ibuprofen = new Talent();
        setTalent(ibuprofen, "Ibuprofen", "cast heal", false, false, true, false, ibuprofenicon, bot_center_left, 5500, -1, 47, -1, -1, -1, -1, -1, -1, -1, -1, 1);
        Biolchem_Talents[2] = ibuprofen;

        Talent influenza = new Talent();
        setTalent(influenza, "Influenza", "DoT", false, true, true, false, influenzaicon, top_right_most, 5000, -1, 48, 5, -1, -1, -1, -1, -1, -1, -1, 1);
        Biolchem_Talents[3] = influenza;

        Talent chlorinegas = new Talent();
        setTalent(chlorinegas, "Chlorine", "DoT cloud", false, true, true, false, chlorinegasicon, mid_right_most, 5000, -1, 49, -1, -1, -1, -1, -1, -1, -1, -1, 1);
        Biolchem_Talents[4] = chlorinegas;

		Talent Myoglobin = new Talent ();
		setTalent (Myoglobin, "Myoglobin", "Scimitar", false, true, true, false, myoglobinicon, top_left_most, 0, -1, 50, -1, -1, 0, -1, -1, -1, -1, -1, 1);
		Biolchem_Talents [5] = Myoglobin;



	}

    
     Rect top_left_most = new Rect(1920 / 2-275, 1080 / 2 -180, 50, 50);
     Rect mid_left_most = new Rect(1920 / 2 -275, 1080 / 2 - 80, 50, 50) ;
     Rect bot_left_most = new Rect(1920 / 2 - 275, 1080 / 2 + 120, 50, 50) ;
     Rect top_center_left =  new Rect(1920 / 2 - 100, 1080 / 2 - 180, 50, 50) ;
     Rect mid_center_left =  new Rect(1920 / 2 - 100, 1080 / 2 - 80, 50, 50) ;
     Rect bot_center_left =  new Rect(1920 / 2 - 100, 1080 / 2 + 120, 50, 50);
     Rect top_center_right = new Rect(1920 / 2 - 25, 1080 / 2 - 180, 50, 50);
     Rect mid_center_right =  new Rect(1920 / 2 - 25, 1080 / 2 - 80, 50, 50);
     Rect bot_center_right =  new Rect(1920 / 2 - 25, 1080 / 2 + 120, 50, 50);
     Rect top_right_most = new Rect(1920 / 2 + 100, 1080 / 2 - 180, 50, 50);
     Rect mid_right_most = new Rect(1920 / 2 + 100, 1080 / 2 - 80, 50, 50);
     Rect bot_right_most =  new Rect(1920 / 2 + 100, 1080 / 2 + 120, 50, 50) ;
    

    // Update is called once per frame
	void Update () {
	    
	}
}
